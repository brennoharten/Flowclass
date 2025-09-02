// src/app/dashboard/aluno/page.tsx
"use client";
import { useQuery } from "@tanstack/react-query";
import api from "@/lib/api";
import Link from "next/link";

type Class = {
  id: string;
  name: string;
};

export default function AlunoDashboard() {
  const { data, isLoading, error } = useQuery<Class[]>({
    queryKey: ["myClasses"],
    queryFn: async () => {
      const res = await api.get("/students/classes");
      return res.data;
    },
  });

  if (isLoading) return <p>Carregando...</p>;
  if (error) return <p>Erro ao carregar turmas.</p>;

  return (
    <div className="p-6">
      <h1 className="text-xl font-bold">Minhas Turmas</h1>
      <ul className="space-y-2 mt-4">
        {data?.map((cls) => (
          <li key={cls.id} className="p-4 border rounded-lg">
            <Link href={`/dashboard/aluno/classes/${cls.id}`}>
              {cls.name}
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
}
