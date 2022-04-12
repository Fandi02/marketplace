using System.Globalization;

namespace marketplace.Helpers;
public static class Common
{
    public static byte[] StreamToBytes(Stream streamContent)
    {
        MemoryStream ms = new MemoryStream();

        streamContent.CopyTo(ms);

        return ms.ToArray();
    }

    public static string ToEmpty(this string content)
    {
        return "";
    }

    public static byte[] ToBytes(this Stream streamContent)
    {
        MemoryStream ms = new MemoryStream();

        streamContent.CopyTo(ms);

        return ms.ToArray();
    }

    public static Tuple<int, int> ToLimitOffset(int? page, int? pageCount){
        int limit = AppConstant.DEFAULT_PAGE_COUNT;
        if(pageCount != null)
        {
            limit = pageCount.Value;
        }

        int offset = 0;
        if(page == null) 
        {
            offset = 0;
        }else{
            offset = (page.Value - 1) * limit;
        }

        return new Tuple<int, int>(limit, offset);
    }    

    public static int ToInt(this string content)
    {
        if (int.TryParse(content, out int result))
        {
            return result;
        }

        throw new InvalidOperationException("Anda belum melakukan login");
    }
    public static string ToIDR(this decimal val){
        return val.ToString("C", new CultureInfo("id-ID"));
    }
}