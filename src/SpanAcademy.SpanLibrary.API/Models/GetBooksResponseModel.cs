using SpanAcademy.SpanLibrary.Application.Books.Models;
using SpanAcademy.SpanLibrary.Application.Collections;

namespace SpanAcademy.SpanLibrary.API.Models
{
    public class GetBooksResponseModel
    {
        public IEnumerable<BookDto> Books { get; private set; }
        public int TotakCount { get; private set; }

        public GetBooksResponseModel(IPagedList<BookDto> bookDtoPagedList)
        {
            Books = bookDtoPagedList;
            TotakCount = bookDtoPagedList.TotalCount;
        }
    }
}
