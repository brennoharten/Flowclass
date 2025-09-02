// src/lib/auth.ts
export function setAccessToken(token: string) {
  localStorage.setItem("accessToken", token);
}

export function getAccessToken() {
  return localStorage.getItem("accessToken");
}

export function removeAccessToken() {
  localStorage.removeItem("accessToken");
}

export function setRefreshToken(token: string) {
  localStorage.setItem("refreshToken", token);
}

export function getRefreshToken() {
  return localStorage.getItem("refreshToken");
}

export function removeRefreshToken() {
  localStorage.removeItem("refreshToken");
}

export function clearTokens() {
  removeAccessToken();
  removeRefreshToken();
}
