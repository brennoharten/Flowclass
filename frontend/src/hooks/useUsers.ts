import { useQuery, useMutation } from "@tanstack/react-query";
import { getCurrentUser, updateUser } from "@/api/users";

export function useCurrentUser() {
  return useQuery({
    queryKey: ["currentUser"],
    queryFn: getCurrentUser,
  });
}

export function useUpdateUser() {
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: any }) =>
      updateUser(id, data),
  });
}
