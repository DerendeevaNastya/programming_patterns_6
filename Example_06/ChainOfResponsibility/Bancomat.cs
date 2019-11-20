using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Example_06.ChainOfResponsibility
{
    public enum CurrencyType
    {
        Eur,
        Dollar,
        Ruble
    }

    public interface IBanknote
    {
        CurrencyType Currency { get; }
        string Value { get; }
    }

    public class Banknote : IBanknote
    {
        public Banknote(CurrencyType currency, string value)
        {
            Currency = currency;
            Value = value;
        }
        public CurrencyType Currency { get; }
        public string Value { get; }
    }

    public static class Changer
    {
        public static string GetChange(int[] banknoteValues, string currency, int moneyValue)
        {
            var operands = new List<string>();
            foreach (var value in banknoteValues.Reverse())
            {
                var count = (int) (moneyValue / value);
                operands.Add(count == 0 ? null : $"{count}*{value}{currency}");
                moneyValue -= value * count;
            }

            return String.Join(" + ", operands.Where(x => x != null));
        }
    }

    public class Bancomat
    {
        private readonly BanknoteHandler _handler;

        public Bancomat()
        {
            _handler = new TenRubleHandler(null);
            _handler = new TenDollarHandler(_handler);
            _handler = new FiftyDollarHandler(_handler);
            _handler = new HundredDollarHandler(_handler);
        }

        public bool Validate(string banknote)
        {
            return _handler.Validate(banknote);
        }

        public List<string> GetCash(IBanknote banknote, List<string> alternatives)
        {
            return _handler.GetCash(banknote, alternatives);
        }
    }

    public abstract class BanknoteHandler
    {
        private readonly BanknoteHandler _nextHandler;

        protected BanknoteHandler(BanknoteHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public virtual bool Validate(string banknote)
        {
            return _nextHandler != null && _nextHandler.Validate(banknote);
        }

        public virtual List<string> GetCash(IBanknote banknote, List<string> alternatives)
        {
            if (_nextHandler == null)
            {
                return alternatives;
            }

            return _nextHandler.GetCash(banknote, alternatives);
        }
    }

    public class TenRubleHandler : BanknoteHandler
    {
        public override bool Validate(string banknote)
        {
            if (banknote.Equals("10 Рублей"))
            {
                return true;
            }

            return base.Validate(banknote);
        }

        public override List<string> GetCash(IBanknote banknote, List<string> alternatives)
        {
            int bValue;
            if (banknote.Currency == CurrencyType.Ruble && Int32.TryParse(banknote.Value, out bValue))
            {
                var banknoteValues = new[] {10, 50, 100, 500, 1000, 5000};
                var str = Changer.GetChange(banknoteValues, "р", bValue);
                alternatives.Add(str);
            }

            return base.GetCash(banknote, alternatives);
        }


        public TenRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public abstract class DollarHandlerBase : BanknoteHandler
    {
        public override bool Validate(string banknote)
        {
            if (banknote.Equals($"{Value}$"))
            {
                return true;
            }

            return base.Validate(banknote);
        }

        protected abstract int Value { get; }

        public override List<string> GetCash(IBanknote banknote, List<string> alternatives)
        {
            int bValue;
            if (banknote.Currency == CurrencyType.Ruble && Int32.TryParse(banknote.Value, out bValue))
            {
                var banknoteValues = new[] {10, 50, 100, 500, 1000};
                var str = Changer.GetChange(banknoteValues, "$", bValue);
                alternatives.Add(str);
            }

            return base.GetCash(banknote, alternatives);
        }

        protected DollarHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class HundredDollarHandler : DollarHandlerBase
    {
        protected override int Value => 100;

        public HundredDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class FiftyDollarHandler : DollarHandlerBase
    {
        protected override int Value => 50;

        public FiftyDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class TenDollarHandler : DollarHandlerBase
    {
        protected override int Value => 10;

        public TenDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }
}