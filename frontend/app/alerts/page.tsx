'use client'

import { useState, useEffect } from 'react'
import { AlertList } from '@/components/alerts/AlertList'
import { AlertForm } from '@/components/alerts/AlertForm'
import { Button } from '@/components/ui/button'
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog'
import { useRouter } from 'next/navigation'
import { toast } from 'sonner'
import { useSession } from 'next-auth/react'
import { LoadingSpinner } from '@/components/LoadingSpinner'

export default function AlertsPage() {
  const [open, setOpen] = useState(false)
  const router = useRouter()
  const { data: session, status } = useSession()

  useEffect(() => {
    if (status === 'unauthenticated') {
      router.push('/auth/login')
    }
  }, [status, router])

  if (status === 'loading') {
    return <LoadingSpinner />
  }

  if (!session) {
    return null
  }

  const handleCreateAlert = async (data: any) => {
    if (!session?.accessToken) {
      toast.error('Oturum açmanız gerekiyor')
      return
    }

    try {
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/PropertyAlerts`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${session.accessToken}`
        },
        body: JSON.stringify({
          ...data,
          minPrice: data.minPrice ? Number(data.minPrice) : null,
          maxPrice: data.maxPrice ? Number(data.maxPrice) : null,
          minBedrooms: data.minBedrooms ? Number(data.minBedrooms) : null,
          minSquareMeters: data.minSquareMeters ? Number(data.minSquareMeters) : null,
        })
      })

      if (!response.ok) {
        const errorData = await response.json()
        throw new Error(errorData.message || 'Bildirim oluşturulurken bir hata oluştu')
      }

      toast.success('Bildirim başarıyla oluşturuldu')
      setOpen(false)
      router.refresh()
    } catch (error) {
      toast.error(error instanceof Error ? error.message : 'Bildirim oluşturulurken bir hata oluştu')
      console.error('Bildirim oluşturma hatası:', error)
    }
  }

  return (
    <div className="container py-10">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Bildirimlerim</h1>
        <Dialog open={open} onOpenChange={setOpen}>
          <DialogTrigger asChild>
            <Button>Yeni Bildirim Oluştur</Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>Yeni Bildirim Oluştur</DialogTitle>
            </DialogHeader>
            <AlertForm onSubmit={handleCreateAlert} />
          </DialogContent>
        </Dialog>
      </div>

      <AlertList />
    </div>
  )
} 