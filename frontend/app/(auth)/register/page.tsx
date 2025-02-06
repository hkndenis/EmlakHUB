'use client'

import { useState } from 'react'
import { useRouter } from 'next/navigation'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import * as z from 'zod'
import { AuthCard } from '@/components/auth/AuthCard'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { Label } from '@/components/ui/label'

const registerSchema = z.object({
  firstName: z.string().min(2, 'Ad en az 2 karakter olmalıdır'),
  lastName: z.string().min(2, 'Soyad en az 2 karakter olmalıdır'),
  email: z.string().email('Geçerli bir email adresi giriniz'),
  password: z.string().min(6, 'Şifre en az 6 karakter olmalıdır'),
})

type RegisterForm = z.infer<typeof registerSchema>

export default function RegisterPage() {
  const router = useRouter()
  const [error, setError] = useState<string>('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  const form = useForm<RegisterForm>({
    resolver: zodResolver(registerSchema),
    defaultValues: {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
    },
  })

  const onSubmit = async (data: RegisterForm) => {
    try {
      setIsSubmitting(true)
      setError('')

      console.log('API isteği gönderiliyor:', data)

      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/auth/register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: data.email,
          password: data.password,
          firstName: data.firstName,
          lastName: data.lastName
        }),
      })

      const result = await response.json()

      if (!response.ok) {
        throw new Error(result.error || 'Kayıt işlemi başarısız')
      }

      router.push('/login?registered=true')
    } catch (err) {
      if (err instanceof Error) {
        setError(err.message)
      } else {
        setError('Kayıt işlemi sırasında bir hata oluştu')
      }
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <AuthCard title="Kayıt Ol">
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <div>
          <Label htmlFor="firstName">Ad</Label>
          <Input
            id="firstName"
            {...form.register('firstName')}
            error={form.formState.errors.firstName?.message}
          />
        </div>

        <div>
          <Label htmlFor="lastName">Soyad</Label>
          <Input
            id="lastName"
            {...form.register('lastName')}
            error={form.formState.errors.lastName?.message}
          />
        </div>

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
          {isSubmitting ? 'Kayıt yapılıyor...' : 'Kayıt Ol'}
        </Button>
      </form>
    </AuthCard>
  )
} 