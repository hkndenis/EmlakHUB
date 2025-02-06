'use client'

import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import * as z from 'zod'
import { PropertyType } from '@/types/enums'
import { Combobox } from '@/components/ui/combobox'
import { useQuery } from '@tanstack/react-query'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Button } from '@/components/ui/button'

// Form şeması
const schema = z.object({
  city: z.string().min(1, 'Şehir seçiniz'),
  district: z.string().optional(),
  minPrice: z.number().optional(),
  maxPrice: z.number().optional(),
  minBedrooms: z.number().optional(),
  minSquareMeters: z.number().optional(),
  type: z.nativeEnum(PropertyType, {
    errorMap: () => ({ message: 'İlan tipi seçiniz' }),
  }),
})

interface AlertFormProps {
  onSubmit: (data: z.infer<typeof schema>) => void
}

export function AlertForm({ onSubmit }: AlertFormProps) {
  const [selectedCity, setSelectedCity] = useState<string>('')
  const form = useForm<z.infer<typeof schema>>({
    resolver: zodResolver(schema),
  })

  // Şehirleri getir
  const { data: cities = [], isLoading: isCitiesLoading } = useQuery({
    queryKey: ['cities'],
    queryFn: async () => {
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/addresses/cities`)
      if (!response.ok) {
        throw new Error('Şehirler yüklenirken hata oluştu')
      }
      const data = await response.json()
      return data.map((city: string) => ({
        label: city,
        value: city,
      }))
    },
  })

  // İlçeleri getir
  const { data: districts = [], isLoading: isDistrictsLoading } = useQuery({
    queryKey: ['districts', selectedCity],
    queryFn: async () => {
      if (!selectedCity) return []
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/addresses/districts?city=${selectedCity}`)
      if (!response.ok) {
        throw new Error('İlçeler yüklenirken hata oluştu')
      }
      const data = await response.json()
      return data.map((district: string) => ({
        label: district,
        value: district,
      }))
    },
    enabled: !!selectedCity,
  })

  const handleSubmit = form.handleSubmit((data) => {
    onSubmit(data)
  })

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div className="grid gap-4 md:grid-cols-2">
        <div>
          <Label>Şehir</Label>
          <Combobox
            options={cities}
            value={form.watch('city')}
            onChange={(value) => {
              form.setValue('city', value)
              setSelectedCity(value)
              form.setValue('district', '') // Şehir değişince ilçeyi sıfırla
            }}
            placeholder={isCitiesLoading ? "Yükleniyor..." : "Şehir seçiniz"}
            disabled={isCitiesLoading}
          />
          {form.formState.errors.city && (
            <p className="text-sm text-destructive">{form.formState.errors.city.message}</p>
          )}
        </div>

        <div>
          <Label>İlçe</Label>
          <Combobox
            options={districts}
            value={form.watch('district')}
            onChange={(value) => form.setValue('district', value)}
            placeholder={isDistrictsLoading ? "Yükleniyor..." : "İlçe seçiniz"}
            disabled={!selectedCity || isDistrictsLoading}
          />
        </div>
      </div>

      <div className="grid gap-4 md:grid-cols-2">
        <div>
          <Label>Min. Fiyat</Label>
          <Input type="number" {...form.register('minPrice', { valueAsNumber: true })} />
        </div>

        <div>
          <Label>Max. Fiyat</Label>
          <Input type="number" {...form.register('maxPrice', { valueAsNumber: true })} />
        </div>
      </div>

      <div className="grid gap-4 md:grid-cols-2">
        <div>
          <Label>Min. Oda Sayısı</Label>
          <Input type="number" {...form.register('minBedrooms', { valueAsNumber: true })} />
        </div>

        <div>
          <Label>Min. m²</Label>
          <Input type="number" {...form.register('minSquareMeters', { valueAsNumber: true })} />
        </div>
      </div>

      <div>
        <Label>İlan Tipi</Label>
        <Combobox
          options={[
            { label: 'Satılık', value: PropertyType.ForSale },
            { label: 'Kiralık', value: PropertyType.ForRent },
          ]}
          value={form.watch('type')}
          onChange={(value) => form.setValue('type', value as PropertyType)}
          placeholder="İlan tipi seçiniz"
        />
        {form.formState.errors.type && (
          <p className="text-sm text-destructive">{form.formState.errors.type.message}</p>
        )}
      </div>

      <Button type="submit" className="w-full">Bildirim Oluştur</Button>
    </form>
  )
} 