// src/api/auth.ts
import api from "@/lib/api";

export async function login(data: { email: string; password: string }) {
  return api.post("/auth/login", data);
}

export async function register(data: { email: string; password: string }) {
  return api.post("/auth/register", data);
}

export async function refresh(refreshToken: string) {
  return api.post("/auth/refresh", { refreshToken });
}

export async function logout() {
  return api.post("/auth/logout");
}
