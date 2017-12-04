using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin;
using NBitcoin.SPV;

namespace CoinBag.Models
{
    public class TransactionDetail : IEquatable<TransactionDetail>
    {
        private WalletTransaction _tx;

        public TransactionDetail(WalletTransaction tx)
        {
            this._tx = tx;
        }

        public string Balance => $"{_tx.Balance.ToUnit(MoneyUnit.BTC)} BTC";
        public string TransactionTime => _tx.BlockInformation?.Header.BlockTime.ToString("yyyy/MM/dd hh:mm:ss") ?? "N/A";
        public string TransactionId => _tx.Transaction.GetHash().ToString();
        public int Confirmations => _tx.BlockInformation?.Confirmations ?? 0;

        public bool Equals(TransactionDetail other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TransactionId == other.TransactionId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TransactionDetail) obj);
        }

        public override int GetHashCode()
        {
            return TransactionId.GetHashCode();
        }
    }
}
