// src/app/(landing)/page.tsx
import { Button } from "@/components/ui/button";
import { Card, CardContent } from "@/components/ui/card";
import Link from "next/link";
import { GraduationCap, Calendar, Users } from "lucide-react";

export default function LandingPage() {
  return (
    <div className="flex flex-col min-h-screen">
      {/* Hero */}
      <section className="flex-1 flex flex-col items-center justify-center text-center px-6 py-20 bg-gradient-to-b from-slate-50 to-white">
        <h1 className="text-5xl font-bold tracking-tight mb-4">
          Gerencie <span className="text-blue-600">aulas e alunos</span> em um só lugar
        </h1>
        <p className="text-lg text-muted-foreground max-w-xl mb-8">
          Plataforma completa para professores organizarem turmas, controlarem
          presença e compartilharem conteúdos. Simples, rápido e moderno.
        </p>
        <div className="flex gap-4">
          <Link href="/auth/register">
            <Button size="lg">Comece grátis</Button>
          </Link>
          <Link href="/auth/login">
            <Button size="lg" variant="outline">
              Entrar
            </Button>
          </Link>
        </div>
      </section>

      {/* Features */}
      <section className="py-20 bg-slate-50">
        <div className="max-w-5xl mx-auto px-6 grid md:grid-cols-3 gap-8 text-center">
          <Feature
            icon={<GraduationCap className="h-10 w-10 text-blue-600 mx-auto" />}
            title="Gestão de turmas"
            desc="Cadastre turmas, aulas e acompanhe o progresso de cada aluno."
          />
          <Feature
            icon={<Calendar className="h-10 w-10 text-blue-600 mx-auto" />}
            title="Agendamento fácil"
            desc="Organize suas aulas com calendário integrado e presença digital."
          />
          <Feature
            icon={<Users className="h-10 w-10 text-blue-600 mx-auto" />}
            title="Colaboração"
            desc="Alunos acessam conteúdos, materiais e acompanham sua frequência."
          />
        </div>
      </section>

      {/* CTA */}
      <section className="py-20 text-center">
        <h2 className="text-3xl font-bold mb-4">Pronto para começar?</h2>
        <p className="text-muted-foreground mb-6">
          Leve sua gestão de aulas para o próximo nível.
        </p>
        <Link href="/auth/register">
          <Button size="lg">Criar conta gratuita</Button>
        </Link>
      </section>

      {/* Footer */}
      <footer className="py-6 text-center text-sm text-muted-foreground border-t">
        © {new Date().getFullYear()} Flowclass. Todos os direitos reservados.
      </footer>
    </div>
  );
}

function Feature({
  icon,
  title,
  desc,
}: {
  icon: React.ReactNode;
  title: string;
  desc: string;
}) {
  return (
    <Card className="shadow-sm">
      <CardContent className="pt-6">
        {icon}
        <h3 className="text-xl font-semibold mt-4">{title}</h3>
        <p className="text-sm text-muted-foreground mt-2">{desc}</p>
      </CardContent>
    </Card>
  );
}
