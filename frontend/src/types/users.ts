export interface User {
  id: string;
  name: string;
  email: string;
  role: "Student" | "Teacher" | "Admin";
}

export interface UpdateUserPayload {
  name?: string;
  email?: string;
  // adicione outros campos que podem ser atualizados
}
