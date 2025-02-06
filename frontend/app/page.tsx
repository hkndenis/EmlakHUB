import Link from 'next/link'
import { Button } from '@/components/ui/button'

export default function HomePage() {
  return (
    <div className="container mx-auto py-12">
      <div className="max-w-3xl mx-auto text-center space-y-8">
        <h1 className="text-4xl font-bold">
          Modern Emlak Platformuna Hoş Geldiniz
        </h1>
        
        <p className="text-xl text-muted-foreground">
          Hayalinizdeki evi bulmak için binlerce ilanı keşfedin
        </p>

        <div className="flex items-center justify-center gap-4">
          <Link href="/properties">
            <Button size="lg">İlanları Görüntüle</Button>
          </Link>
          <Link href="/properties/create">
            <Button size="lg" variant="outline">İlan Ver</Button>
          </Link>
        </div>
      </div>
    </div>
  )
} 