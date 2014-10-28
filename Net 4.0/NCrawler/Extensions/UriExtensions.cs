using System;

using NCrawler.Utils;

namespace NCrawler.Extensions
{
	public static class UriExtensions
	{
		#region Class Methods

		public static string GetUrlKeyString(this Uri uri, UriComponents uriSensitivity)
		{
			// Get complete url
			string completeUrl = uri.ToString().ToUpperInvariant();

			// Get sensitive part
			string sensitiveUrlPart = uri.GetComponents(uriSensitivity, UriFormat.Unescaped);
            //Console.WriteLine("completeUrl: " + completeUrl);
            //Console.WriteLine("uriSensitivity: " + uriSensitivity);
            //Console.WriteLine("sensitiveUrlPart: " + sensitiveUrlPart);
			if (sensitiveUrlPart.IsNullOrEmpty())
			{
				return completeUrl;
			}
            var result = completeUrl.Replace(sensitiveUrlPart.ToUpperInvariant(), sensitiveUrlPart);
            //Remove any hash tags if it's empty hash tags.
            //Console.WriteLine("result: " + result);
            if (result.Contains("#"))
            {
                result = result.Substring(0, result.IndexOf("#"));
                //Console.WriteLine("uri.Fragment: " + uri.Fragment);
                //if (uri.Fragment == "#")
                //{
                //    result = result.Substring(0, result.IndexOf("#"));
                //}
            }
            //Console.WriteLine("result: " + result);
            return result;
		}

		/// <summary>
		/// Checks if url is the same as the base url
		/// </summary>
		/// <param name="uriBase">Base Uri</param>
		/// <param name="uri">url to check</param>
		/// <returns>Returns true if url is not same as base url, else false</returns>
		public static bool IsHostMatch(this Uri uriBase, Uri uri)
		{
			AspectF.Define.
				NotNull(uriBase, "uriBase");

			if (uri.IsNull())
			{
				return false;
			}

			return !uriBase.Host.Equals(uri.Host, StringComparison.OrdinalIgnoreCase);
		}

		#endregion
	}
}