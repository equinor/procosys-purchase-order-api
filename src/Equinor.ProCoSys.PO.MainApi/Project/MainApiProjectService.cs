﻿using System;
using System.Net;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.MainApi.Client;
using Microsoft.Extensions.Options;

namespace Equinor.ProCoSys.PO.MainApi.Project
{
    public class MainApiProjectService : IProjectApiService
    {
        private readonly string _apiVersion;
        private readonly Uri _baseAddress;
        private readonly IBearerTokenApiClient _mainApiClient;

        public MainApiProjectService(IBearerTokenApiClient mainApiClient,
            IOptionsMonitor<MainApiOptions> options)
        {
            _mainApiClient = mainApiClient;
            _apiVersion = options.CurrentValue.ApiVersion;
            _baseAddress = new Uri(options.CurrentValue.BaseAddress);
        }

        public async Task<ProCoSysProject> TryGetProjectAsync(string plant, string name)
        {
            var url = $"{_baseAddress}ProjectByName" +
                $"?plantId={plant}" +
                $"&projectName={WebUtility.UrlEncode(name)}" +
                $"&api-version={_apiVersion}";

            return await _mainApiClient.TryQueryAndDeserializeAsync<ProCoSysProject>(url);
        }
    }
}
