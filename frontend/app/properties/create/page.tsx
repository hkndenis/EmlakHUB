'use client'

import { useState } from 'react'
import { useRouter } from 'next/navigation'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import * as z from 'zod'
import { PropertyForm } from '@/components/properties/PropertyForm'
import { useSession } from 'next-auth/react'

export default function CreatePropertyPage() {
  const router = useRouter()
  const { data: session } = useSession()
  const [isSubmitting, setIsSubmitting] = useState(false)

  const onSubmit = async (data) => {
    try {
      setIsSubmitting(true)
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/properties`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${session?.accessToken}`
        },
        body: JSON.stringify(data)
      })

      if (!response.ok) throw new Error('İlan oluşturulurken bir hata oluştu')

      router.push('/properties')
    } catch (error) {
      console.error(error)
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <div className="container mx-auto py-8">
      <h1 className="text-2xl font-bold mb-6">Yeni İlan Oluştur</h1>
      <PropertyForm onSubmit={onSubmit} isSubmitting={isSubmitting} />
    </div>
  )
} 