using System;
using System.Collections.Generic;
using System.Linq;

public class PagedList<T> : List<T>
{
	public int CurrentPage { get; private set; }
	public int TotalPages { get; private set; }
	public int PageSize { get; private set; }
	public int TotalCount { get; private set; }

	public bool HasPrevious => CurrentPage > 1;
	public bool HasNext => CurrentPage < TotalPages;

	public PagedList(List<T> items, int count, int pageNumber, int pageSize)
	{
		TotalCount = count;
		PageSize = pageSize;
		CurrentPage = pageNumber;
		TotalPages = (int)Math.Ceiling(count / (double)pageSize);
		AddRange(items);
	}

	public static PagedListWrapper<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
	{
		var count = source.Count();
		var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

		PagedListWrapper<T> pagedListWapper = new PagedListWrapper<T>();
		pagedListWapper.Source = new PagedList<T>(items, count, pageNumber, pageSize);
		pagedListWapper.TotalCount = count;
		pagedListWapper.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
		pagedListWapper.CurrentPage = pageNumber;
		pagedListWapper.PageSize = pageSize;
		return pagedListWapper;
	}



}
public class PagedListWrapper<T>
{
	public PagedList<T> Source { get; set; }
	public int CurrentPage { get; set; }
	public int TotalPages { get; set; }
	public int PageSize { get; set; }
	public int TotalCount { get; set; }

}