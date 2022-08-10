using SpanAcademy.SpanLibrary.Application.Books.Models;
using SpanAcademy.SpanLibrary.Application.Collections;

namespace SpanAcademy.SpanLibrary.API.Models
{
    public class GetBooksResponseModel
    {
        public IEnumerable<BookDto> Books { get; private set; }
        public int TotalCount { get; private set; }

        public GetBooksResponseModel(IPagedList<BookDto> bookDtoPagedList)
        {
            Books = bookDtoPagedList;
            TotalCount = bookDtoPagedList.TotalCount;
        }
    }
}
