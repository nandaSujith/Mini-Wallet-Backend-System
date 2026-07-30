using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MiniWallet.Domain.Entities
{
    public class Wallet
    {
        public int WalletId { get; set; }

        public int UserId { get; set; }

        public decimal Balance { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        public User User { get; set; } = null!;

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
