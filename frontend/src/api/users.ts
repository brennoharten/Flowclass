import api from "@/lib/api";
import type { User, UpdateUserPayload } from "@/types/users";

export async function getCurrentUser(): Promise<User> {
  const { data } = await api.get<User>("/users/me");
  return data;
}

export async function updateUser(id: string, data: UpdateUserPayload): Promise<User> {
  const { data: updated } = await api.put<User>(`/users/${id}`, data);
  return updated;
}
