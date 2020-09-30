using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public class AzureKeyVaultService
    {
        public string GetKeyVaultSecret(string secretName)
        {
            var azureServiceTokenProvider1 = new AzureServiceTokenProvider();
            var keyVault = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider1.KeyVaultTokenCallback));
            var secret = keyVault.GetSecretAsync(secretName).Result;
            return secret.Value;
        }
    }
}
