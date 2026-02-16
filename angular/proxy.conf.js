// Proxy configuration for Angular CLI
// Reads API URL from environment variable set by Aspire
const apiUrl = process.env.services__api__https__0 || process.env.services__api__http__0 || 'https://localhost:44342';

module.exports = {
  '/api': {
    target: apiUrl,
    secure: false,
    changeOrigin: true
  }
};
