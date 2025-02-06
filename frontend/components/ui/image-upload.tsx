'use client'

import { useCallback } from 'react'
import { useDropzone } from 'react-dropzone'
import Image from 'next/image'
import { X } from 'lucide-react'
import { Button } from '@/components/ui/button'

interface ImageUploadProps {
  value: string[]
  onChange: (value: string[]) => void
  onRemove: (value: string) => void
}

export function ImageUpload({ value, onChange, onRemove }: ImageUploadProps) {
  const onDrop = useCallback(async (acceptedFiles: File[]) => {
    try {
      const formData = new FormData()
      acceptedFiles.forEach((file) => {
        formData.append('files', file)
      })

      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/files/upload`, {
        method: 'POST',
        body: formData,
      })

      if (!response.ok) {
        throw new Error('Fotoğraf yüklenirken hata oluştu')
      }

      const data = await response.json()
      onChange([...value, ...data.urls])
    } catch (error) {
      console.error('Fotoğraf yükleme hatası:', error)
    }
  }, [value, onChange])

  const { getRootProps, getInputProps, isDragActive } = useDropzone({
    onDrop,
    accept: {
      'image/*': ['.jpeg', '.jpg', '.png', '.webp']
    },
    maxSize: 5 * 1024 * 1024, // 5MB
  })

  return (
    <div>
      <div
        {...getRootProps()}
        className={`
          border-2 border-dashed rounded-lg p-4 text-center cursor-pointer
          transition-colors duration-200 ease-in-out
          ${isDragActive ? 'border-primary bg-primary/5' : 'border-gray-300 hover:border-primary'}
        `}
      >
        <input {...getInputProps()} />
        {isDragActive ? (
          <p className="text-sm text-gray-600">Fotoğrafları buraya bırakın...</p>
        ) : (
          <div className="space-y-2">
            <p className="text-sm text-gray-600">
              Fotoğrafları sürükleyip bırakın veya seçmek için tıklayın
            </p>
            <p className="text-xs text-gray-500">
              Desteklenen formatlar: JPEG, JPG, PNG, WEBP (Max: 5MB)
            </p>
          </div>
        )}
      </div>

      {value.length > 0 && (
        <div className="mt-4 grid grid-cols-2 gap-4 sm:grid-cols-3 md:grid-cols-4">
          {value.map((url) => (
            <div key={url} className="group relative aspect-square">
              <Image
                src={url}
                alt="Property"
                className="rounded-lg object-cover"
                fill
                sizes="(max-width: 640px) 50vw, (max-width: 768px) 33vw, 25vw"
              />
              <Button
                type="button"
                variant="destructive"
                size="icon"
                className="absolute top-2 right-2 opacity-0 group-hover:opacity-100 transition-opacity"
                onClick={() => onRemove(url)}
              >
                <X className="h-4 w-4" />
              </Button>
            </div>
          ))}
        </div>
      )}
    </div>
  )
} 