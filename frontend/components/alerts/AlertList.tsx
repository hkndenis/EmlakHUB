'use client'

import { useQuery } from '@tanstack/react-query'
import { useSession } from 'next-auth/react'
import { AlertCard } from './AlertCard'
import { Button } from '@/components/ui/button'
import { PropertyType } from '@/types/enums'
import { LoadingSpinner } from '@/components/ui/loading-spinner'

interface Alert {
  id: string
  city: string
  district?: string
  minPrice?: number
  maxPrice?: number
  minBedrooms?: number
  minSquareMeters?: number
  type: PropertyType
  isActive: boolean
}

export function AlertList() {
  const { data: session } = useSession()

  const { data: alerts = [], isLoading } = useQuery({
    queryKey: ['alerts'],
    queryFn: async () => {
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/PropertyAlerts`, {
        headers: {
          'Authorization': `Bearer ${session?.accessToken}`
        }
      })
      if (!response.ok) {
        throw new Error('Bildirimler yüklenirken hata oluştu')
      }
      return response.json()
    },
    enabled: !!session?.accessToken
  })

  if (isLoading) {
    return <LoadingSpinner />
  }

  if (alerts.length === 0) {
    return (
      <div className="text-center py-10">
        <p className="text-gray-500">Henüz bildirim oluşturmadınız</p>
      </div>
    )
  }

  return (
    <div className="space-y-4">
      {alerts.map((alert: Alert) => (
        <AlertCard key={alert.id} alert={alert} />
      ))}
    </div>
  )
} 