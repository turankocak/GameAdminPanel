import React, { useState, useEffect } from 'react';
import buildingService from '../services/buildingService';
import '../assets/styles/ConfigurationPage.css';

const ConfigurationPage = () => {
    const [buildings, setBuildings] = useState([]);
    const [buildingTypes, setBuildingTypes] = useState([]);
    const [buildingType, setBuildingType] = useState('');
    const [buildingCost, setBuildingCost] = useState('');
    const [constructionTime, setConstructionTime] = useState('');
    const [modalVisible, setModalVisible] = useState(false);
    const [editingBuilding, setEditingBuilding] = useState(null);

    useEffect(() => {
        loadBuildingTypes();
        loadBuildings();
    }, []);

    const loadBuildingTypes = async () => {
        try {
            const result = await buildingService.getBuildingTypes();
            setBuildingTypes(result.data);
        } catch (error) {
            console.error('Hata:', error);
        }
    };

    const loadBuildings = async () => {
        try {
            const result = await buildingService.getBuildings();
            console.log("Buildings:", result.data); // Gelen veriyi kontrol edin
            setBuildings(result.data);
        } catch (error) {
            console.error('Hata:', error);
        }
    };

    const handleAddBuilding = async (e) => {
        e.preventDefault();
        console.log('Form gönderiliyor'); // Bu log ekleyin

        if (!buildingType || buildingCost.trim() === '' || constructionTime.trim() === '') {
            alert('Tüm alanları doldurduğunuzdan emin olun.');
            return;
        }

        const cost = parseFloat(buildingCost);
        const time = parseInt(constructionTime, 10);

        if (isNaN(cost) || cost <= 0) {
            alert('Bina maliyeti sıfır veya negatif olamaz.');
            return;
        }
        if (isNaN(time) || time < 30 || time > 1800) {
            alert('İnşaat süresi minimum 30 saniye ve maksimum 1800 saniye olmalıdır.');
            return;
        }

        try {
            const buildingData = { buildingType, cost, constructionTime: time };
            console.log('Gönderilen veri:', buildingData); // Gönderilen veriyi kontrol edin

            if (editingBuilding) {
                buildingData.id = editingBuilding.id;  // ID'yi ekleyin
                await buildingService.updateBuilding(editingBuilding.id, buildingData);
                setEditingBuilding(null);
            } else {
                await buildingService.addBuilding(buildingData);
            }
            loadBuildings();
            setBuildingType('');
            setBuildingCost('');
            setConstructionTime('');
            setModalVisible(false);
        } catch (error) {
            console.error('Hata:', error.response ? error.response.data : error.message);
            alert('Hata: ' + (error.response ? JSON.stringify(error.response.data) : error.message));
        }
    };

    const handleEditBuilding = (building) => {
        setEditingBuilding(building);
        setBuildingType(building.buildingType);
        setBuildingCost(building.cost); // cost kullanılıyor
        setConstructionTime(building.constructionTime);
        setModalVisible(true);
    };

    const handleDeleteBuilding = async (id) => {
        console.log("Silinmek istenen ID (fonksiyonda):", id); // ID'yi fonksiyon içinde kontrol edin
        if (!id || id.length !== 24) {
            alert('Geçersiz ID.');
            return;
        }
        try {
            await buildingService.deleteBuilding(id);
            loadBuildings();
        } catch (error) {
            console.error('Hata:', error.response?.data || error.message);
            alert('Hata: ' + (error.response?.data || error.message));
        }
    };

    const closeModal = () => setModalVisible(false);

    // Filtrelenmiş bina türlerini döndüren fonksiyon
    const getFilteredBuildingTypes = () => {
        const existingTypes = new Set(buildings.map(b => b.buildingType));
        return buildingTypes.filter(type => !existingTypes.has(type.type));
    };

    return (
        <div className="configuration-container">
            <h2>Yönetici Paneli - Bina Ayarları</h2>
            <button onClick={() => { setEditingBuilding(null); setModalVisible(true); }}>
                Ekle
            </button>

            {modalVisible && (
                <div id="addBuildingModal" className="modal">
                    <div className="modal-content">
                        <span className="modal-close" onClick={closeModal}>&times;</span>
                        <h3>{editingBuilding ? 'Bina Düzenle' : 'Yeni Bina Ekle'}</h3>
                        <form onSubmit={handleAddBuilding}>
                            <select
                                value={buildingType}
                                onChange={(e) => setBuildingType(e.target.value)}
                                required
                            >
                                <option value="">Bina Türü Seçin</option>
                                {getFilteredBuildingTypes().length > 0 ? (
                                    getFilteredBuildingTypes().map((type) => (
                                        <option key={type.id} value={type.type}>{type.type}</option>
                                    ))
                                ) : (
                                    <option>Yükleniyor...</option>
                                )}
                            </select>
                            <input
                                type="number"
                                placeholder="Bina Maliyeti"
                                value={buildingCost}
                                onChange={(e) => setBuildingCost(e.target.value)}
                                required
                            />
                            <input
                                type="number"
                                placeholder="İnşaat Süresi (sn)"
                                value={constructionTime}
                                onChange={(e) => setConstructionTime(e.target.value)}
                                required
                            />
                            <button type="submit">{editingBuilding ? 'Güncelle' : 'Tamam'}</button>
                        </form>
                    </div>
                </div>
            )}

            <div className="card">
                <table>
                    <thead>
                        <tr>
                            <th>Bina Türü</th>
                            <th>Bina Maliyeti</th>
                            <th>İnşaat Süresi</th>
                            <th>İşlemler</th>
                        </tr>
                    </thead>
                    <tbody>
                        {buildings.map((building) => (
                            <tr key={building.id}>
                                <td>{building.buildingType}</td>
                                <td>{building.cost}</td> 
                                <td>{building.constructionTime}</td>
                                <td>
                                    <button onClick={() => handleEditBuilding(building)} className="edit-btn">Düzenle</button>
                                    <button onClick={() => {
                                                                console.log("Silinmek istenen ID:", building.id); 
                                                                handleDeleteBuilding(building.id);
                                                            }} className="delete-btn">Sil</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default ConfigurationPage;
