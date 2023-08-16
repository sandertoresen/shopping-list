import axios, { Axios } from "axios";

// const instance = axios.create({
//     baseURL: 'https://some-domain.com/api/',
//     timeout: 1000,
//     headers: {'X-Custom-Header': 'foobar'}
//   });
// http://127.0.0.1:5000/shopping-list
// ApiService.js
class ApiService {
    constructor() {
        this.baseURL = import.meta.env.VITE_REACT_APP_BASE_URL_API;
        this.port = import.meta.env.VITE_REACT_APP_API_PORT;

        this.completeBaseURL = `${this.baseURL}:${this.port}`;

        this.instance = axios.create({
            baseURL: this.completeBaseURL,
            timeout: 1000,
            headers: {
                "Content-Type": "application/json",
            },
        });
    }

    async getItems() {
        try {
            const response = await this.instance.get("/shopping-list");
            return response.data;
        } catch (error) {
            console.error("Error fetching items:", error);
            throw error;
        }
    }

    async addItem(itemName, count) {
        try {
            const response = await this.instance.post("/shopping-list", {
                name: itemName,
                count: count,
            });
            return response.data;
        } catch (error) {
            console.error("Error adding items:", error);
            throw error;
        }
    }

    async updateItem(id, addedValue) {
        try {
            const response = await this.instance.put("/shopping-list", {
                id: id,
                added: addedValue,
            });
            return response.data;
        } catch (error) {
            console.error("Error updating item:", error);
            throw error;
        }
    }

    async deleteItem(id) {
        try {
            const response = await this.instance.delete("/shopping-list", {
                params: { id: id },
            });
            return response.data;
        } catch (error) {
            console.error("Error deleting item:", error);
            throw error;
        }
    }
}

export default ApiService;
