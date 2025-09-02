// src/app/(auth)/register/page.tsx
"use client";
import { useState } from "react";
import api from "@/lib/api";
import { setAccessToken, setRefreshToken } from "@/lib/auth";
import { useRouter } from "next/navigation";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";

export default function RegisterPage() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const router = useRouter();

  async function handleRegister(e: React.FormEvent) {
    e.preventDefault();
    if (password !== confirmPassword) {
      alert("As senhas não coincidem!");
      return;
    }

    const res = await api.post("/auth/register", { name, email, password });

    // já loga direto após registrar
    setAccessToken(res.data.accessToken);
    setRefreshToken(res.data.refreshToken);

    router.push("/dashboard");
  }

  return (
    <form onSubmit={handleRegister} className="max-w-sm mx-auto mt-20 space-y-4">
      <Input
        placeholder="Nome completo"
        value={name}
        onChange={(e) => setName(e.target.value)}
        required
      />
      <Input
        type="email"
        placeholder="Email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
      />
      <Input
        type="password"
        placeholder="Senha"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        required
      />
      <Input
        type="password"
        placeholder="Confirme a senha"
        value={confirmPassword}
        onChange={(e) => setConfirmPassword(e.target.value)}
        required
      />
      <Button type="submit" className="w-full">
        Criar conta
      </Button>
    </form>
  );
}
