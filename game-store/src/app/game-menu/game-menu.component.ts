import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { GameService } from '../services/game.service';
import { CartService } from '../services/cart.service';
import { WishlistService } from '../services/wishlist.service';
@Component({
  selector: 'app-game-menu',
  imports: [CommonModule, FormsModule],
  templateUrl: './game-menu.component.html',
  styleUrl: './game-menu.component.css'
})
export class GameMenuComponent {
  activeTab: string = 'all';
  searchQuery: string = '';
  showFilters: boolean = false;
  itemsToShow: number = 6;
  quickViewGame: any = null;
  detailsExpanded: boolean = false;
  buyGame: any = null;
  
  gameItems = [
    { 
      id: 1,
      name: 'Atomfall', 
      category: 'action',
      price: 59.99, 
      description: 'A futuristic action-packed RPG with cyber-enhanced warriors.',
      image: 'https://pub-f354ec240bea480db7320bd0e29d972e.r2.dev/sites/2/2025/03/Atomfall-hero-d25f8e2fb0c6e97799e1.jpg',
      rating: 4.8,
      reviews: 1500,
      downloads: 200000,
      badge: 'Top Seller',
      genre: 'Action RPG',
      isFavorite: false,
      platforms: ['PC', 'PS5', 'Xbox'],
    },
    { 
      id: 2,
      name: 'Racing Rivals', 
      category: 'racing',
      price: 49.99, 
      description: 'High-speed adrenaline-fueled racing with realistic physics.',
      image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTrGksnJpXiB0xcE4ROvTcnFjS7rZziRL4wWw&s',
      rating: 4.5,
      reviews: 980,
      downloads: 500000,
      badge: 'Best Racing Game',
      genre: 'Racing',
      isFavorite: false,
      platforms: ['PC', 'PS5', 'Xbox'],
    },
    { 
      id: 3,
      name: 'Mystic Quest', 
      category: 'adventure',
      price: 39.99, 
      description: 'An open-world fantasy adventure filled with mythical creatures.',
      image: 'https://thefinalfantasy.net/gallery/wallpaper/ff-mystic-quest/ff-mystic-quest-wallpaper-2.jpg',
      rating: 4.7,
      reviews: 2000,
      downloads: 300000,
      badge: 'Editor\'s Choice',
      genre: 'Adventure RPG',
      isFavorite: false,
      platforms: ['PC', 'Switch'],
    },
    { 
      id: 4,
      name: 'Mini Royale: The Ultimate Toy Soldier Shooter!', 
      category: 'shooter',
      price: 69.99, 
      description: 'Mini Royale elevates the beloved toy soldier experience with creative weapons, gadgets, and dynamic movement. Swing from curtain rods, dodge between towering action figures, and zip across the room using the game’s signature grapple gun. Every match offers fresh and exciting ways to outmaneuver your opponents.',
      image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXVIFt6G3VaWO1iR0G4bjdn-bna6nFddmRrg&s',
      rating: 4.6,
      reviews: 3000,
      downloads: 1000000,
      badge: 'Top Multiplayer',
      genre: 'Shooter',
      isFavorite: false,
      platforms: ['PC', 'PS5', 'Xbox'],
    },
    { 
      id: 5,
      name: 'Assassin’s Creed Shadows', 
      category: 'adventure',
      price: 29.99, 
      description: 'Experience an epic action-adventure story set in feudal Japan! Become a lethal shinobi assassin and powerful legendary samurai as you explore a beautiful open world in a time of chaos.',
      image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT4M0BgASIn8R5h1LlbKEjSbLP4Yj2VBnnmQw&s',
      rating: 4.4,
      reviews: 800,
      downloads: 150000,
      badge: 'Best Strategy Game',
      genre: 'Strategy',
      isFavorite: false,
      platforms: ['PC'],
    },
    { 
      id: 6,
      name: '33 Immortals', 
      category: 'action',
      price: 59.99, 
      description: 'A futuristic action-packed RPG with cyber-enhanced warriors.',
      image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8Pl5St9OkeKrTXM53NOX5K36bALgetvbztA&s',
      rating: 4.8,
      reviews: 1500,
      downloads: 200000,
      badge: 'Top Seller',
      genre: 'Action RPG',
      isFavorite: false,
      platforms: ['PC', 'PS5', 'Xbox'],
    },
    { 
      id: 7,
      name: 'HOT WHEELS™ - Sportscars Pack', 
      category: 'racing',
      price: 59.99, 
      description: 'When speed is everything, everyone wants the fastest cars! Get Track Manga™ and take advantage of its massive spoilers for crazy drift stunts, or ignite the power of GT-Scorcher™ in the heat of the race!',
      image: 'https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1633700/header.jpg?t=1706115603',
      rating: 4.0,
      reviews: 150,
      downloads: 20000,
      badge: 'Top Seller',
      genre: 'Action RPG',
      isFavorite: false,
      platforms: ['PC', 'PS5', 'Xbox'],
    },
    { 
      id: 8,
      name: 'Warpath: Ace Shooter', 
      category: 'shooter',
      price: 99.99, 
      description: 'Your mission: To strike at the very heart of Raven. You will fight thrilling battles on both the ground and in the air. Equipped with an arsenal of powerful modern weapons, you will infiltrate deep behind enemy lines to ambush targets where they least expect it. You must become the embodiment of "death from afar": picking your spots, waiting for the right moment, then pulling the trigger.',
      image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDJpbdJrU6nzAtpG9liGxyXs45fwQ298W1RQ&s',
      rating: 4.6,
      reviews: 3000,
      downloads: 1000000,
      badge: 'Top Multiplayer',
      genre: 'Shooter',
      isFavorite: false,
      platforms: ['PC', 'PS5', 'Xbox'],
    },
    
  ];

  filteredGames: any[] = [];
  cartItems: any[] = [];

  constructor(private router: Router, 
    private cartService: CartService,
private wishlistService: WishlistService) {
    this.filterGames();
  }

  clearSearch() {
    this.searchQuery = '';
    this.filterGames();
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
    this.filterGames();
  }

  toggleFilters() {
    this.showFilters = !this.showFilters;
  }

  filterGames() {
    this.filteredGames = this.gameItems.filter(game => {
      const categoryMatch = this.activeTab === 'all' || game.category === this.activeTab;
      const searchMatch = !this.searchQuery || game.name.toLowerCase().includes(this.searchQuery.toLowerCase()) || game.description.toLowerCase().includes(this.searchQuery.toLowerCase());
      return categoryMatch && searchMatch;
    });
  }

  onSearch() {
    this.filterGames();
  }

  toggleFavorite(game: any) {
    game.isFavorite = !game.isFavorite;
  }

  openQuickView(game: any) {
    this.quickViewGame = { ...game, quantity: 1 };
    this.detailsExpanded = false;
    document.body.style.overflow = 'hidden';
  }

  closeQuickView() {
    this.quickViewGame = null;
    document.body.style.overflow = '';
  }


  loadMore() {
    this.itemsToShow += 6;
  }

  getVisibleGames() {
    return this.filteredGames.slice(0, this.itemsToShow);
  }
  
  

  getCategoryTitle(category: string): string {
    switch (category) {
      case 'action': return 'Action Games';
      case 'racing': return 'Racing Games';
      case 'adventure': return 'Adventure Games';
      case 'sports': return 'Sports Games';
      case 'shooter': return 'Shooter Games';
      default: return 'All Games';
    }
  }

  getGamesByCategory(category: string) {
    return this.filteredGames.filter(game => game.category === category);
  }

  getUniqueCategories(): string[] {
    const categories = new Set(this.filteredGames.map(game => game.category));
    return Array.from(categories).sort().reverse();
  }
  
  
  addToCart(game: any) {
    this.cartService.addToCart(game);  // Use service instead of local array
    this.router.navigate(['/cart']);   // Navigate after adding
  }
  addToWishlist(game: any) {
    this.wishlistService.addToWishlist(game);
    alert(`${game.title} added to wishlist!`);
    this.router.navigate(['/wishlist']); // Navigate to wishlist page
  }
  

}

