using System;
using System.Threading.Tasks;
using System.Net;

namespace Xamarin.Forms.Example
{
	public static class HttpExtensions
	{
		public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
		{
			var taskComplete = new TaskCompletionSource<HttpWebResponse>();
			request.BeginGetResponse(asyncResponse =>
				{
					try
					{
						HttpWebRequest responseRequest = (HttpWebRequest)asyncResponse.AsyncState;
						HttpWebResponse someResponse = (HttpWebResponse)responseRequest.EndGetResponse(asyncResponse);
						taskComplete.TrySetResult(someResponse);
					}
					catch (WebException webExc)
					{
						HttpWebResponse failedResponse = (HttpWebResponse)webExc.Response;
						taskComplete.TrySetResult(failedResponse);
					}
				}, request);
			return taskComplete.Task;
		}
	}
}

