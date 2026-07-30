using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWallet.Application.DTOs
{
    public class WithdrawDto
    {
        public decimal Amount { get; set; }
        public string ReferenceId { get; set; } = string.Empty;
    }
}
