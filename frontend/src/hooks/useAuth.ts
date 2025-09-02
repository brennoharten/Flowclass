"use client";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import api from "@/lib/api";
import {
  setAccessToken,
  setRefreshToken,
  clearTokens,
} from "@/lib/auth";

// -----------------------------
// LOGIN
// -----------------------------
export function useLogin() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({ email, password }: { email: string; password: string }) => {
      const res = await api.post("/auth/login", { email, password });
      setAccessToken(res.data.accessToken);
      setRefreshToken(res.data.refreshToken);
      return res.data;
    },
    onSuccess: () => {
      // invalida dados do usuário logado
      queryClient.invalidateQueries({ queryKey: ["user"] });
    },
  });
}

// -----------------------------
// REGISTER
// -----------------------------
export function useRegister() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({
      name,
      email,
      password,
    }: { name: string; email: string; password: string }) => {
      const res = await api.post("/auth/register", { name, email, password });
      return res.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["user"] });
    },
  });
}

// -----------------------------
// LOGOUT
// -----------------------------
export function useLogout() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async () => {
      await api.post("/auth/logout");
      clearTokens();
    },
    onSuccess: () => {
      queryClient.clear(); // limpa tudo do cache
    },
  });
}
