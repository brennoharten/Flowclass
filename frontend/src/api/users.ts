// src/api/users.ts
import api from "@/lib/api";

export async function getCurrentUser() {
  return api.get("/users/me");
}

export async function updateUser(id: string, data: any) {
  return api.put(`/users/${id}`, data);
}
