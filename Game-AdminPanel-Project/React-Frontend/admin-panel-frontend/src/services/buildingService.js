import axios from 'axios';

const API_URL = 'http://localhost:5113/api/building';

// Bina tiplerini getiren API çağrısı
const getBuildingTypes = () => {
    return axios.get(`${API_URL}/types`);
};

// Binaları getiren API çağrısı
const getBuildings = () => {
    return axios.get(API_URL);
};

// Yeni bir bina ekleyen API çağrısı
const addBuilding = (buildingData) => {
    return axios.post(API_URL, buildingData);
};

// Var olan bir binayı güncelleyen API çağrısı
const updateBuilding = (id, buildingData) => {
    return axios.put(`${API_URL}/${id}`, buildingData);
};

// Var olan bir binayı silen API çağrısı
const deleteBuilding = (id) => {
    return axios.delete(`${API_URL}/${id}`);
};

export default {
    getBuildingTypes,
    getBuildings,
    addBuilding,
    updateBuilding,
    deleteBuilding,
};
