using marketplace.Interfaces;
using marketplace.Datas;
using marketplace.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using marketplace.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace marketplace.Services;

[Authorize(Roles = AppConstant.CUSTOMER_ROLE)]
public class AlamatService : BaseDbService, IAlamatService
{
    public AlamatService(marketplaceContext dbContext) : base(dbContext)
    {
    }

    public async Task<Alamat> Add(Alamat obj, int idAlamat)
     {
          if (await dbContext.Alamats.AnyAsync(x => x.IdAlamat == obj.IdAlamat))
          {
               throw new InvalidOperationException($"Alamat with ID {obj.IdAlamat} is already exist");
          }

          await dbContext.AddAsync(obj);
          await dbContext.SaveChangesAsync();

          dbContext.Alamats.Add(new Alamat
          {
               IdAlamat = idAlamat,
               IdPembeli = obj.IdPembeli,
               Provinsi = obj.Provinsi,
               Kota = obj.Kota,
               Kecamatan = obj.Kecamatan,
               Desa = obj.Desa,
               KodePos = obj.KodePos,
               Deskripsi = obj.Deskripsi,
            //    IdPembeliNavigation = obj.IdPembeliNavigation
          });

          return obj;
     }

     public async Task<Alamat> Add(Alamat obj)
     {
          if (await dbContext.Alamats.AnyAsync(x => x.IdAlamat == obj.IdAlamat))
          {
               throw new InvalidOperationException($"Alamat with ID {obj.IdAlamat} is already exist");
          }

          await dbContext.AddAsync(obj);
          await dbContext.SaveChangesAsync();

          return obj;
     }

     public async Task<bool> Delete(int id)
     {
          var Alamat = await dbContext.Alamats.FirstOrDefaultAsync(x => x.IdAlamat == id);

          if (Alamat == null)
          {
               throw new InvalidOperationException($"Alamat with ID {id} doesn't exist");
          }

          dbContext.Alamats.RemoveRange(dbContext.Alamats.Where(x => x.IdAlamat == id));
          dbContext.Remove(Alamat);
          await dbContext.SaveChangesAsync();

          return true;
     }

     public async Task<List<Alamat>> Get(int limit, int offset, string keyword)
     {
          if (string.IsNullOrEmpty(keyword))
          {
               keyword = "";
          }

          return await dbContext.Alamats.Skip(offset).Take(limit).ToListAsync();
     }

     public async Task<Alamat?> Get(int id)
     {
          var result = await dbContext.Alamats.FirstOrDefaultAsync(x => x.IdAlamat == id);
          if (result == null)
          {
               throw new InvalidOperationException($"Alamat with ID {id} doesn't exist");
          }
          return result;
     }

     async Task<List<AlamatViewModel>> IAlamatService.GetAlamat(int IdPembeli)
     {
          var result = await (from a in dbContext.Alamats
                              where a.IdPembeli == IdPembeli
                              select new AlamatViewModel
                              {
                                   IdAlamat = a.IdAlamat,
                                   IdPembeli = a.IdPembeli,
                                    Provinsi = a.Provinsi,
                                    Kota = a.Kota,
                                    Kecamatan = a.Kecamatan,
                                    Desa = a.Desa,
                                    KodePos = a.KodePos,
                                    Deskripsi = a.Deskripsi
                              }).ToListAsync();
          return result;
     }

     public Task<Alamat?> Get(Expression<Func<Alamat, bool>> func)
     {
          throw new NotImplementedException();
     }

     public async Task<Alamat> Update(Alamat obj)
     {
          if (obj == null)
          {
               throw new ArgumentException("Alamat cannot be null");
          }

          var result = await dbContext.Alamats.FirstOrDefaultAsync(x => x.IdAlamat == obj.IdAlamat);

          if (result == null)
          {
               throw new InvalidOperationException($"Alamat with ID{obj.IdAlamat} doesn't exist in database");
          }
            result.IdAlamat = obj.IdAlamat;
            result.IdPembeli = obj.IdPembeli;
            result.Provinsi = obj.Provinsi;
            result.Kota = obj.Kota;
            result.Kecamatan = obj.Kecamatan;
            result.Desa = obj.Desa;
            result.KodePos = obj.KodePos;
            result.Deskripsi = obj.Deskripsi;

          dbContext.Update(result);
          await dbContext.SaveChangesAsync();

          return result;
     }
     public async Task<List<Alamat>> GetAll(int IdPembeli)
     {
          return await dbContext.Alamats.Where(x => x.IdPembeli == IdPembeli).ToListAsync();
     }
     public async Task<List<Alamat>> GetAll()
     {
          return await dbContext.Alamats.ToListAsync();
     }
}