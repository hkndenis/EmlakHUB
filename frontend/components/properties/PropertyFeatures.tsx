interface PropertyFeature {
  id: string
  name: string
  description: string
}

interface PropertyFeaturesProps {
  features: PropertyFeature[]
}

export function PropertyFeatures({ features }: PropertyFeaturesProps) {
  return (
    <div className="mt-8">
      <h2 className="text-lg font-semibold mb-4">Özellikler</h2>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        {features.map((feature) => (
          <div key={feature.id} className="p-4 bg-gray-50 rounded-lg">
            <div className="font-medium">{feature.name}</div>
            {feature.description && (
              <div className="text-sm text-gray-500 mt-1">
                {feature.description}
              </div>
            )}
          </div>
        ))}
      </div>
    </div>
  )
} 