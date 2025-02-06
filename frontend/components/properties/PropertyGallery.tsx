'use client'

import { useState } from 'react'
import Image from 'next/image'
import { getImageUrl } from '@/lib/utils'

interface PropertyGalleryProps {
  images: Array<{ id: string; url: string; isMain: boolean }>
}

export function PropertyGallery({ images }: PropertyGalleryProps) {
  const [selectedImage, setSelectedImage] = useState(images[0]?.url)

  return (
    <div className="space-y-4">
      {/* Ana görsel */}
      <div className="aspect-video relative rounded-lg overflow-hidden">
        {selectedImage ? (
          <Image
            src={getImageUrl(selectedImage)}
            alt="Property"
            fill
            className="object-cover"
          />
        ) : (
          <div className="w-full h-full bg-gray-200 flex items-center justify-center">
            <span className="text-gray-400">Görsel yok</span>
          </div>
        )}
      </div>

      {/* Küçük görseller */}
      <div className="grid grid-cols-6 gap-2">
        {images.map((image) => (
          <button
            key={image.id}
            onClick={() => setSelectedImage(image.url)}
            className={`aspect-square relative rounded-md overflow-hidden ${
              selectedImage === image.url ? 'ring-2 ring-primary' : ''
            }`}
          >
            <Image
              src={image.url}
              alt="Property thumbnail"
              fill
              className="object-cover"
            />
          </button>
        ))}
      </div>
    </div>
  )
} 