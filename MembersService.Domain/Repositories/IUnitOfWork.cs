using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IMemberRepository Members { get; }
        IMemberProgressRepository MemberProgress { get; }

        /// <summary>
        /// حفظ جميع التغييرات في قاعدة البيانات كوحدة واحدة (Transaction)
        /// </summary>
        Task<int> CommitAsync();

        /// <summary>
        /// التراجع عن التغييرات إذا فشل التنفيذ
        /// </summary>
        void Rollback();
    }
}
