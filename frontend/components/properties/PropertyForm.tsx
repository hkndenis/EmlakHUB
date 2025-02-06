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
import { Textarea } from '@/components/ui/textarea'
import { ImageUpload } from '@/components/ui/image-upload'
import { MultiSelect } from '@/components/ui/multi-select'

// Form şeması
const schema = z.object({
  title: z.string().min(1, 'Başlık gereklidir'),
  description: z.string().min(1, 'Açıklama gereklidir'),
  price: z.number().min(1, 'Fiyat gereklidir'),
  city: z.string().min(1, 'Şehir seçiniz'),
  district: z.string().min(1, 'İlçe seçiniz'),
  address: z.string().min(1, 'Adres gereklidir'),
  type: z.nativeEnum(PropertyType, {
    errorMap: () => ({ message: 'İlan tipi seçiniz' }),
  }),
  bedrooms: z.number().min(0, 'Oda sayısı 0 veya daha büyük olmalıdır'),
  bathrooms: z.number().min(0, 'Banyo sayısı 0 veya daha büyük olmalıdır'),
  squareMeters: z.number().min(1, 'Metrekare gereklidir'),
  features: z.array(z.string()).default([]),
  images: z.array(z.string()).min(1, 'En az 1 fotoğraf gereklidir'),
})

interface PropertyFormProps {
  initialData?: z.infer<typeof schema>
  onSubmit: (data: z.infer<typeof schema>) => void
  isLoading?: boolean
}

export function PropertyForm({ initialData, onSubmit, isLoading }: PropertyFormProps) {
  const [selectedCity, setSelectedCity] = useState<string>(initialData?.city || '')
  const form = useForm<z.infer<typeof schema>>({
    resolver: zodResolver(schema),
    defaultValues: initialData || {
      images: [],
      features: [],
    },
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

  return (
    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
      <div>
        <Label>Fotoğraflar</Label>
        <ImageUpload
          value={form.watch('images')}
          onChange={(urls) => form.setValue('images', urls)}
          onRemove={(url) => form.setValue('images', form.watch('images').filter((current) => current !== url))}
        />
        {form.formState.errors.images && (
          <p className="text-sm text-destructive">{form.formState.errors.images.message}</p>
        )}
      </div>

      <div>
        <Label>Başlık</Label>
        <Input {...form.register('title')} />
        {form.formState.errors.title && (
          <p className="text-sm text-destructive">{form.formState.errors.title.message}</p>
        )}
      </div>

      <div>
        <Label>Açıklama</Label>
        <Textarea {...form.register('description')} />
        {form.formState.errors.description && (
          <p className="text-sm text-destructive">{form.formState.errors.description.message}</p>
        )}
      </div>

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
          {form.formState.errors.district && (
            <p className="text-sm text-destructive">{form.formState.errors.district.message}</p>
          )}
        </div>
      </div>

      <div>
        <Label>Adres</Label>
        <Textarea {...form.register('address')} />
        {form.formState.errors.address && (
          <p className="text-sm text-destructive">{form.formState.errors.address.message}</p>
        )}
      </div>

      <div className="grid gap-4 md:grid-cols-2">
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

        <div>
          <Label>Fiyat</Label>
          <Input type="number" {...form.register('price', { valueAsNumber: true })} />
          {form.formState.errors.price && (
            <p className="text-sm text-destructive">{form.formState.errors.price.message}</p>
          )}
        </div>
      </div>

      <div className="grid gap-4 md:grid-cols-3">
        <div>
          <Label>Oda Sayısı</Label>
          <Input type="number" {...form.register('bedrooms', { valueAsNumber: true })} />
          {form.formState.errors.bedrooms && (
            <p className="text-sm text-destructive">{form.formState.errors.bedrooms.message}</p>
          )}
        </div>

        <div>
          <Label>Banyo Sayısı</Label>
          <Input type="number" {...form.register('bathrooms', { valueAsNumber: true })} />
          {form.formState.errors.bathrooms && (
            <p className="text-sm text-destructive">{form.formState.errors.bathrooms.message}</p>
          )}
        </div>

        <div>
          <Label>m²</Label>
          <Input type="number" {...form.register('squareMeters', { valueAsNumber: true })} />
          {form.formState.errors.squareMeters && (
            <p className="text-sm text-destructive">{form.formState.errors.squareMeters.message}</p>
          )}
        </div>
      </div>

      <div>
        <Label>Özellikler</Label>
        <MultiSelect
          options={[
            { label: 'Balkon', value: 'balcony' },
            { label: 'Asansör', value: 'elevator' },
            { label: 'Otopark', value: 'parking' },
            { label: 'Güvenlik', value: 'security' },
            { label: 'Havuz', value: 'pool' },
            { label: 'Spor Salonu', value: 'gym' },
            { label: 'Merkezi Isıtma', value: 'central_heating' },
            { label: 'Doğalgaz', value: 'natural_gas' },
            { label: 'Eşyalı', value: 'furnished' },
            { label: 'Beyaz Eşya', value: 'white_goods' },
            { label: 'Klima', value: 'air_conditioning' },
            { label: 'İnternet', value: 'internet' },
          ]}
          value={form.watch('features')}
          onChange={(value) => form.setValue('features', value)}
          placeholder="Özellik seçiniz"
        />
      </div>

      <Button type="submit" className="w-full" disabled={isLoading}>
        {isLoading ? 'Kaydediliyor...' : initialData ? 'Güncelle' : 'İlan Oluştur'}
      </Button>
    </form>
  )
} 