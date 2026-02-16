import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';
const apiUrl = 'http://localhost:44341';

const oAuthConfig = {
  issuer: apiUrl + '/',
  redirectUri: baseUrl,
  clientId: 'Chaos_App',
  responseType: 'code',
  scope: 'offline_access Chaos',
  requireHttps: false,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Chaos',
  },
  oAuthConfig,
  apis: {
    default: {
      url: apiUrl,
      rootNamespace: 'Chaos',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
