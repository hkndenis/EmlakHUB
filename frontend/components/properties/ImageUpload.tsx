'use client'

import { useState, useRef } from 'react'
import Image from 'next/image'
import { Button } from '@/components/ui/button'
import { getImageUrl } from '@/lib/utils'

interface ImageUploadProps {
  images: Array<{ id: string; url: string; isMain: boolean }>
  onChange: (images: Array<{ id: string; url: string; isMain: boolean }>) => void
  maxImages?: number
}

export function ImageUpload({ images, onChange, maxImages = 6 }: ImageUploadProps) {
  const [isUploading, setIsUploading] = useState(false)
  const fileInputRef = useRef<HTMLInputElement>(null)

  const handleFileSelect = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files
    if (!files?.length) return

    setIsUploading(true)
    try {
      const formData = new FormData()
      Array.from(files).forEach(file => {
        formData.append('files', file)
      })

      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/properties/upload-images`, {
        method: 'POST',
        body: formData
      })

      if (!response.ok) throw new Error('Resim yükleme başarısız')

      const newImages = await response.json()
      onChange([...images, ...newImages])
    } catch (error) {
      console.error('Resim yükleme hatası:', error)
    } finally {
      setIsUploading(false)
      if (fileInputRef.current) {
        fileInputRef.current.value = ''
      }
    }
  }

  const handleRemoveImage = (index: number) => {
    const newImages = [...images]
    newImages.splice(index, 1)
    onChange(newImages)
  }

  const handleSetMainImage = (index: number) => {
    const newImages = images.map((img, i) => ({
      ...img,
      isMain: i === index
    }))
    onChange(newImages)
  }

  return (
    <div className="space-y-4">
      <div className="grid grid-cols-3 gap-4">
        {images.map((image, index) => (
          <div key={image.id} className="relative aspect-video">
            <Image
              src={getImageUrl(image.url)}
              alt={`Property image ${index + 1}`}
              fill
              className="object-cover rounded-lg"
            />
            <div className="absolute top-2 right-2 space-x-2">
              <Button
                size="sm"
                variant={image.isMain ? "default" : "secondary"}
                onClick={() => handleSetMainImage(index)}
              >
                {image.isMain ? 'Ana Görsel' : 'Ana Görsel Yap'}
              </Button>
              <Button
                size="sm"
                variant="destructive"
                onClick={() => handleRemoveImage(index)}
              >
                Sil
              </Button>
            </div>
          </div>
        ))}
      </div>

      {images.length < maxImages && (
        <div>
          <input
            ref={fileInputRef}
            type="file"
            accept="image/*"
            multiple
            onChange={handleFileSelect}
            className="hidden"
          />
          <Button
            type="button"
            variant="outline"
            onClick={() => fileInputRef.current?.click()}
            disabled={isUploading}
            className="w-full"
          >
            {isUploading ? 'Yükleniyor...' : 'Resim Yükle'}
          </Button>
        </div>
      )}
    </div>
  )
} 