'use client'

import { useState } from 'react'
import { useRouter } from 'next/navigation'
import { PropertyForm } from '@/components/properties/PropertyForm'
import { toast } from 'sonner'

export default function NewPropertyPage() {
  const router = useRouter()
  const [isLoading, setIsLoading] = useState(false)

  const handleSubmit = async (data: any) => {
    try {
      setIsLoading(true)
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/properties`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      })

      if (!response.ok) {
        throw new Error('İlan oluşturulurken bir hata oluştu')
      }

      toast.success('İlan başarıyla oluşturuldu')
      router.push('/properties')
    } catch (error) {
      console.error('İlan oluşturma hatası:', error)
      toast.error('İlan oluşturulurken bir hata oluştu')
    } finally {
      setIsLoading(false)
    }
  }

  return (
    <div className="container max-w-3xl py-10">
      <h1 className="text-2xl font-bold mb-6">Yeni İlan Oluştur</h1>
      <PropertyForm onSubmit={handleSubmit} isLoading={isLoading} />
    </div>
  )
} 