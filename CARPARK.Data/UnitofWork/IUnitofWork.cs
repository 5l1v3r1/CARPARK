using CARPARK.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Data.UnitofWork
{
    public interface IUnitofWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Değişiklikleri Kaydet
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Transaction Başlat.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Kayıt esnasında bir sorun olmaz ise transaction durdur.
        /// </summary>
        void Commit();

        /// <summary>
        /// Kayıt esnasında bir sorun olursa değişiklikleri geri al.
        /// </summary>
        void Rollback();
    }
}
