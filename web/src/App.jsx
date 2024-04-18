import { useEffect, useState } from 'react'
import './App.css'
import ShoppingList from './components/ShoppingList';
import ApiService from './services/ApiService';

function App() {
  const apiService = new ApiService;

  const [shoppingList, setList] = useState(0);

  useEffect(() => {
    const fetchShoppingList = async () => {

      try {
        const shoppingList = await apiService.getItems();
        console.log(shoppingList);
        setList(shoppingList);
      } catch (error) {
        console.error('Error fetching shopping list:', error);
      }
    };

    fetchShoppingList();

  }, []);

  return (
    <>
      <h1>Shopping list</h1>
      <ShoppingList shoppingList={shoppingList} setList={setList} apiService={apiService} />
    </>
  )
}

export default App
