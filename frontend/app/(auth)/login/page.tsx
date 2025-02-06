'use client'

import { useState } from 'react'
import { useRouter } from 'next/navigation'
import { signIn } from 'next-auth/react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import * as z from 'zod'
import { AuthCard } from '@/components/auth/AuthCard'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { Label } from '@/components/ui/label'

const loginSchema = z.object({
  email: z.string().email('Geçerli bir email adresi giriniz'),
  password: z.string().min(6, 'Şifre en az 6 karakter olmalıdır'),
})

type LoginForm = z.infer<typeof loginSchema>

export default function LoginPage() {
  const router = useRouter()
  const [error, setError] = useState<string>('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  const form = useForm<LoginForm>({
    resolver: zodResolver(loginSchema),
  })

  const onSubmit = async (data: LoginForm) => {
    try {
      setIsSubmitting(true)
      setError('')

      const result = await signIn('credentials', {
        email: data.email,
        password: data.password,
        redirect: false,
      })

      if (result?.error) {
        setError('Email veya şifre hatalı')
        return
      }

      router.push('/')
      router.refresh()
    } catch (err) {
      setError('Giriş yapılırken bir hata oluştu')
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <AuthCard title="Giriş Yap">
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <div>
          <Label htmlFor="email">E-posta</Label>
          <Input
            id="email"
            type="email"
            {...form.register('email')}
            error={form.formState.errors.email?.message}
          />
        </div>

        <div>
          <Label htmlFor="password">Şifre</Label>
          <Input
            id="password"
            type="password"
            {...form.register('password')}
            error={form.formState.errors.password?.message}
          />
        </div>

        {error && (
          <div className="text-sm text-red-500">{error}</div>
        )}

        <Button
          type="submit"
          className="w-full"
          disabled={isSubmitting}
        >
          {isSubmitting ? 'Giriş yapılıyor...' : 'Giriş Yap'}
        </Button>
      </form>
    </AuthCard>
  )
} 