import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
let URL = "https://test-be.ammag.tech/api/v1"

// Create an instance of Axios with your base URL and configuration
const ServerApi = axios.create({
  baseURL: URL,
});

// Add a request interceptor to add headers or perform any other request-related tasks
ServerApi.interceptors.request.use(
  async (config) => {
    try {
      // Retrieve the token from local storage
      const token = await AsyncStorage.getItem('Token');
      console.log("token :::: ",token)

      // Set the token as the Authorization header
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }

      return config;
    } catch (error) {
      return Promise.reject(error);
    }
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Add a response interceptor to handle responses or perform any other response-related tasks
ServerApi.interceptors.response.use(
  (response) => {
    // You can handle successful responses here
    return response;
  },
  (error) => {
    // Handle error responses here
    if (error.response) {
      // The request was made and the server responded with a non-2xx status code
    } else if (error.request) {
      // The request was made but no response was received
    } else {
      // Something happened in setting up the request that triggered an error
    }

    // You can handle errors here or re-throw the error
    return Promise.reject(error.response || error);
  }
);

export default ServerApi;
