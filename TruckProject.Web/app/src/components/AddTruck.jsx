import React, { useState, useEffect  } from "react";
import TruckDataService from "../services/TruckService";
import TruckModelDataService from "../services/ModelTruckService";

const AddTruck = () => {
  const initialTutorialState = {
    productId: null,
    modelTruckId: "",
    fabricationYear: 0,
    chassi: "",
    modelYear: 0,
    listPrice: null
  };

  const [tutorial, setTutorial] = useState(initialTutorialState);
  const [TruckModel, setTruckModel] = useState([]);
  const [message, setMessage] = useState("");
  const [submitted, setSubmitted] = useState(false);

  useEffect(() => {
    retrievetruckModels();
  }, []);
 
  const retrievetruckModels = () => {
    TruckModelDataService.getAll()
      .then(response => {
        setTruckModel(response.data);
        setMessage("");
      })
      .catch(e => {
        setMessage("Error loading truck model: " + e.response.data.errors);
      });
  };

  const handleInputChange = event => {
    const { name, value } = event.target;
    setTutorial({ ...tutorial, [name]: value });
  };

  const saveTutorial = () => {
    var data = {
      id: null,
      modelTruckId: tutorial.modelTruckId,
      fabricationYear: tutorial.fabricationYear,
      modelYear: tutorial.modelYear,
      chassi: tutorial.chassi,
      modelTruck: null
    };

    if(tutorial.modelTruckId == 0 ||  tutorial.fabricationYear == 0 ||
      tutorial.modelYear == 0){
        setMessage("Only Chassi can be null");
    }
    else{

      TruckDataService.create(data)
        .then(response => {
          setSubmitted(true);
          console.log(response.data);
        })
        .catch(error => {
          if (error.response) {

            setMessage("Error saving: " + error.response.data.errors);

        } else if (error.request) {
          
            setMessage("Error saving: ");

        } else {
            
          
            console.log('Error saving: ', error.message);
        }
        console.log(error);
        });
    }
  };
  
  const newTutorial = () => {
    setTutorial(initialTutorialState);
    setSubmitted(false);
  };

  return (
    
    <div className="submit-form">
      {submitted ? (
        <div>
          <h4>Truck successfully saved!</h4>
          <button className="btn btn-success" onClick={newTutorial}>
            Add
          </button>
        </div>
      ) : (
        
        <div>
          <h4>Create Truck</h4>
          <div className="form-group">
            <label htmlFor="fabricationYear">Fabrication Year</label>
            <input
              type="text"
              className="form-control"
              id="fabricationYear"
              required
              value={tutorial.fabricationYear}
              onChange={handleInputChange}
              name="fabricationYear"
            />
          </div>

          <div className="form-group">
            <label htmlFor="chassi">Chassi</label>
            <input
              type="text"
              className="form-control"
              id="chassi"
              value={tutorial.chassi}
              onChange={handleInputChange}
              name="chassi"
            />
          </div>

          <div className="form-group">
            <label htmlFor="modelYear">Model Year</label>
            <input
              type="text"
              className="form-control"
              id="modelYear"
              required
              value={tutorial.modelYear}
              onChange={handleInputChange}
              name="modelYear"
            />
          </div>

          <div className="form-group">
            <select name="modelTruckId" defaultValue="1" onChange={handleInputChange}>
            <option name="modelTruckId" value={0}>Select Model Truck</option>
              {TruckModel.map((item, index) => <option key={index} name="modelTruckId" value={item.id}>{item.model}</option>)}
            </select>
          </div>
          <div>
            {message.length > 0 && <p style={{color:'red'}}>Mensagem: {message}</p>}
          </div>

          <button onClick={saveTutorial} className="btn btn-success">
            Submit
          </button>
        </div>
      )}
    </div>
  );
};

export default AddTruck;