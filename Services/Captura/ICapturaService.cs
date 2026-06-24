using AuthService.Models;

namespace AuthService.Services.Captura;

public interface ICapturaService
{
    List<CapturaRecord> GetAll();
    CapturaRecord GetById(int id);
    CapturaRecord Add(CapturaRecord record);
    CapturaRecord Update(CapturaRecord record);
    bool Delete(int id);
}