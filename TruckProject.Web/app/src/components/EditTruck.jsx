import React, { useState, useEffect  } from "react";
import TruckDataService from "../services/TruckService";
import TruckModelDataService from "../services/ModelTruckService";
import TruckService from "../services/TruckService";
import { useParams, useHistory  } from "react-router-dom";

const EditTruck = () => {
  const initialTutorialState = {
    productId: null,
    modelTruckId: "",
    fabricationYear: 0,
    chassi: "",
    modelYear: 0,
    listPrice: null
  };

  const { id } = useParams();
  const [tutorial, setTutorial] = useState(initialTutorialState);
  const [TruckModel, setTruckModel] = useState([]);
  const [message, setMessage] = useState("");
  const [submitted, setSubmitted] = useState(false);
  const history = useHistory();

  useEffect(() => {
    get(id);
    getTruckModels();
  }, []);
 
   const get = id => {
    TruckService.get(id)
      .then(response => {
        setTutorial(response.data);
        console.log(response.data);
        setMessage("");
      })
      .catch(e => {
        console.log(e);
        setMessage("Error loading information: " + e.response.data.errors);
      });
  }; 

  const getTruckModels = id => {
    TruckModelDataService.getAll()
      .then(response => {
        setTruckModel(response.data);
        console.log(response.data);
        setMessage("");
      })
      .catch(e => {
        console.log(e);
        setMessage("Error loading truck model: " + e.response.data.errors);
      });
  }; 

  const handleInputChange = event => {
    const { name, value } = event.target;
    setTutorial({ ...tutorial, [name]: value });
  };

  const saveTutorial = () => {
    var data = {
      id: tutorial.id,
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
    TruckDataService.update(data)
      .then(response => {
        setSubmitted(true);
        console.log(response.data);
      })
      .catch(e => {
        setMessage("Error updating: " + e.response.data.errors);
      });
    }
  };

  function handleClick() {
    history.push("/tutorials");
  }

  return (
    <div className="submit-form">
      {submitted ? (
        <div>
          <h4>Truck successfully updated!</h4>
          <button className="btn btn-success" onClick={handleClick}>
            Go Home
          </button>
        </div>
      ) : (
        <div>
          <h4>Edit Truck</h4>
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
              required
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
            {message.length > 0 && <p style={{color:'red'}}>{message}</p>}
          </div>

          <button onClick={saveTutorial} className="btn btn-success">
            Submit
          </button>
        </div>
      )}
    </div>
  );
};

export default EditTruck;