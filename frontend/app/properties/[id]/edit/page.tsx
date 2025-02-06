'use client'

import { useState } from 'react'
import { useRouter } from 'next/navigation'
import { useQuery } from '@tanstack/react-query'
import { PropertyForm } from '@/components/properties/PropertyForm'
import { toast } from 'sonner'
import { LoadingSpinner } from '@/components/ui/loading-spinner'

interface EditPropertyPageProps {
  params: {
    id: string
  }
}

export default function EditPropertyPage({ params }: EditPropertyPageProps) {
  const router = useRouter()
  const [isLoading, setIsLoading] = useState(false)

  // İlan detaylarını getir
  const { data: property, isLoading: isFetching } = useQuery({
    queryKey: ['property', params.id],
    queryFn: async () => {
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/properties/${params.id}`)
      if (!response.ok) {
        throw new Error('İlan bilgileri alınırken hata oluştu')
      }
      return response.json()
    },
  })

  const handleSubmit = async (data: any) => {
    try {
      setIsLoading(true)
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/properties/${params.id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      })

      if (!response.ok) {
        throw new Error('İlan güncellenirken bir hata oluştu')
      }

      toast.success('İlan başarıyla güncellendi')
      router.push('/properties')
    } catch (error) {
      console.error('İlan güncelleme hatası:', error)
      toast.error('İlan güncellenirken bir hata oluştu')
    } finally {
      setIsLoading(false)
    }
  }

  if (isFetching) {
    return (
      <div className="flex justify-center items-center min-h-[400px]">
        <LoadingSpinner />
      </div>
    )
  }

  return (
    <div className="container max-w-3xl py-10">
      <h1 className="text-2xl font-bold mb-6">İlanı Düzenle</h1>
      <PropertyForm 
        initialData={property} 
        onSubmit={handleSubmit} 
        isLoading={isLoading} 
      />
    </div>
  )
} 