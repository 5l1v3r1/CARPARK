using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///  Sorgu işlemleri burada yapılıyor.GetAll motodu burada kullanılıyor.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Id'ye göre veri cekme işlemini find ile yapıyoruz.
        /// </summary>
        /// <param name="id"></param>
        TEntity Find(int id);

        /// <summary>
        /// Görderilen entity'e göre ekleme işlemi yapılıyor.
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Görderilen entity'e göre güncelleme işlemi yapılıyor.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Görderilen entity'e göre silme işlemi yapılıyor.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Görderilen Id'e göre silme işlemi yapılıyor.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

    }
}
