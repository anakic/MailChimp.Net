// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListLogic.cs" company="Brandon Seydel">
//   N/A
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailChimp.Net.Core;
using MailChimp.Net.Core.Responses;
using MailChimp.Net.Interfaces;
using MailChimp.Net.Models;
#pragma warning disable 1584,1711,1572,1581,1580

namespace MailChimp.Net.Logic;

/// <summary>
/// The list logic.
/// </summary>
internal class ListLogic : BaseLogic, IListLogic
{

    public ListLogic(MailChimpOptions mailChimpConfiguration)
        : base(mailChimpConfiguration)
    {
    }

    /// <summary>
    /// The add or update async.
    /// </summary>
    /// <param name="list">
    /// The list.
    /// </param>
    /// <returns>
    /// The <see cref="Task"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref>
    ///         <name>uriString</name>
    ///     </paramref>
    ///     is null. </exception>
    /// <exception cref="UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.<paramref name="uriString" /> is empty.-or- The scheme specified in <paramref name="uriString" /> is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
    /// <exception cref="MailChimpException">
    /// Custom Mail Chimp Exception
    /// </exception>
    public async Task<List> AddOrUpdateAsync(List list, CancellationToken cancellationToken = default)
    {
        using var client = CreateMailClient("lists/");
        System.Net.Http.HttpResponseMessage response;
        if (string.IsNullOrWhiteSpace(list.Id))
        {
            response = await client.PostAsJsonAsync(string.Empty, list, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            response = await client.PatchAsJsonAsync(list.Id, list, cancellationToken).ConfigureAwait(false);
        }

        await response.EnsureSuccessMailChimpAsync().ConfigureAwait(false);

        return await response.Content.ReadAsAsync<List>().ConfigureAwait(false);
    }

    public async Task<List> UpdateAsync(string id, Dictionary<string, object> listData, CancellationToken cancellationToken = default)
    {
        using var client = CreateMailClient("lists/");
        System.Net.Http.HttpResponseMessage response;
        response = await client.PatchAsJsonAsync(id, listData, cancellationToken).ConfigureAwait(false);
        await response.EnsureSuccessMailChimpAsync().ConfigureAwait(false);
        return await response.Content.ReadAsAsync<List>().ConfigureAwait(false);
    }

    public async Task<List> AddAsync(Dictionary<string, object> listData, CancellationToken cancellationToken = default)
    {
        using var client = CreateMailClient("lists/");
        System.Net.Http.HttpResponseMessage response;
        response = await client.PostAsJsonAsync(string.Empty, listData, cancellationToken).ConfigureAwait(false);
        await response.EnsureSuccessMailChimpAsync().ConfigureAwait(false);
        return await response.Content.ReadAsAsync<List>().ConfigureAwait(false);
    }

    /// <summary>
    /// The delete async.
    /// </summary>
    /// <param name="listId">
    /// The list id.
    /// </param>
    /// <returns>
    /// The <see cref="Task"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref>
    ///         <name>uriString</name>
    ///     </paramref>
    ///     is null. </exception>
    /// <exception cref="UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.<paramref name="uriString" /> is empty.-or- The scheme specified in <paramref name="uriString" /> is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
    /// <exception cref="InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
    /// <exception cref="MailChimpException">
    /// Custom Mail Chimp Exception
    /// </exception>
    public async Task DeleteAsync(string listId, CancellationToken cancellationToken = default)
    {
        using var client = CreateMailClient("lists/");
        var response = await client.DeleteAsync(listId, cancellationToken).ConfigureAwait(false);
        await response.EnsureSuccessMailChimpAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// The get all async.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Task"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref>
    ///         <name>uriString</name>
    ///     </paramref>
    ///     is null. </exception>
    /// <exception cref="ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
    /// <exception cref="UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.<paramref name="uriString" /> is empty.-or- The scheme specified in <paramref name="uriString" /> is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
    /// <exception cref="NotSupportedException"><paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
    /// <exception cref="MailChimpException">
    /// Custom Mail Chimp Exception
    /// </exception>
    /// <exception cref="TypeLoadException">A custom attribute type cannot be loaded. </exception>
    public async Task<IEnumerable<List>> GetAllAsync(ListRequest request = null, CancellationToken cancellationToken = default)
        => (await GetResponseAsync(request, cancellationToken).ConfigureAwait(false))?.Lists;

    public IEnumerable<List> GetAll(ListRequest request = null)
        => GetResponse(request)?.Lists;

    /// <summary>
    /// The get all async.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Task"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref>
    ///         <name>uriString</name>
    ///     </paramref>
    ///     is null. </exception>
    /// <exception cref="ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
    /// <exception cref="UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.<paramref name="uriString" /> is empty.-or- The scheme specified in <paramref name="uriString" /> is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
    /// <exception cref="NotSupportedException"><paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
    /// <exception cref="MailChimpException">
    /// Custom Mail Chimp Exception
    /// </exception>
    /// <exception cref="TypeLoadException">A custom attribute type cannot be loaded. </exception>
    public async Task<ListResponse> GetResponseAsync(ListRequest request = null, CancellationToken cancellationToken = default)
    {
        request ??= new ListRequest
        {
            Limit = _limit
        };

        using var client = CreateMailClient("lists");
        var response = await client.GetAsync(request.ToQueryString(), cancellationToken).ConfigureAwait(false);
        await response.EnsureSuccessMailChimpAsync().ConfigureAwait(false);

        var listResponse = await response.Content.ReadAsAsync<ListResponse>().ConfigureAwait(false);
        return listResponse;
    }

    public ListResponse GetResponse(ListRequest request = null)
    {
        request ??= new ListRequest
        {
            Limit = _limit
        };

        using var client = CreateMailClient("lists");
        var response = client.Get(request.ToQueryString());
        response.EnsureSuccessMailChimp();

        var listResponse = response.Content.ReadAs<ListResponse>();
        return listResponse;
    }


    /// <summary>
    /// The get async.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Task"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref>
    ///         <name>uriString</name>
    ///     </paramref>
    ///     is null. </exception>
    /// <exception cref="UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.<paramref name="uriString" /> is empty.-or- The scheme specified in <paramref name="uriString" /> is not correctly formed. See <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- <paramref name="uriString" /> contains too many slashes.-or- The password specified in <paramref name="uriString" /> is not valid.-or- The host name specified in <paramref name="uriString" /> is not valid.-or- The file name specified in <paramref name="uriString" /> is not valid. -or- The user name specified in <paramref name="uriString" /> is not valid.-or- The host or authority name specified in <paramref name="uriString" /> cannot be terminated by backslashes.-or- The port number specified in <paramref name="uriString" /> is not valid or cannot be parsed.-or- The length of <paramref name="uriString" /> exceeds 65519 characters.-or- The length of the scheme specified in <paramref name="uriString" /> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="uriString" />.-or- The MS-DOS path specified in <paramref name="uriString" /> must start with c:\\.</exception>
    /// <exception cref="MailChimpException">
    /// Custom Mail Chimp Exception
    /// </exception>
    public async Task<List> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        using var client = CreateMailClient("lists/");
        var response = await client.GetAsync($"{id}", cancellationToken).ConfigureAwait(false);
        await response.EnsureSuccessMailChimpAsync().ConfigureAwait(false);

        return await response.Content.ReadAsAsync<List>().ConfigureAwait(false);
    }

    public async Task<ListActivityResponse> GetActivityAsync(string listId, QueryableBaseRequest request = null, CancellationToken cancellationToken = default)
    {
        const string BaseUrl = "/lists/{0}/activity";

        request ??= new QueryableBaseRequest
        {
            Limit = _limit
        };

        using var client = CreateMailClient(string.Format(BaseUrl + "/", listId));
        var response = await client.GetAsync(request.ToQueryString(), cancellationToken).ConfigureAwait(false);
        await response.EnsureSuccessMailChimpAsync().ConfigureAwait(false);

        var listActivityResponse = await response.Content.ReadAsAsync<ListActivityResponse>().ConfigureAwait(false);
        return listActivityResponse;
    }

    /// <summary>
    /// Batch subscribe or unsubscribe list members.
    /// </summary>
    public async Task<BatchListResponse> BatchAsync(BatchList batchList, string listId, CancellationToken cancellationToken = default)
    {
        using var client = CreateMailClient($"lists/{listId}");
        var response = await client.PostAsJsonAsync(string.Empty, batchList, cancellationToken).ConfigureAwait(false);

        await response.EnsureSuccessMailChimpAsync().ConfigureAwait(false);
        return await response.Content.ReadAsAsync<BatchListResponse>().ConfigureAwait(false);
    }
}