namespace rift.web.Rest.Interfaces
{
	public interface IPageable
	{
		bool IsPaged
		{
			get;
		}

		int PageNumber
		{
			get;
		}

		int PageSize
		{
			get;
		}

		int Offset
		{
			get;
		}

		IPageable Next
		{
			get;
		}

		IPageable PreviousOrFirst
		{
			get;
		}

		IPageable First
		{
			get;
		}

		bool HasPrevious
		{
			get;
		}
	}
}
