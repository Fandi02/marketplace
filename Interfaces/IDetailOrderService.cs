using marketplace.Datas.Entities;

namespace marketplace.Controllers
{
    public interface IDetailOrderService
    {
        Task<List<DetailOrder>> GetAll(int IdOrder);
    }
}