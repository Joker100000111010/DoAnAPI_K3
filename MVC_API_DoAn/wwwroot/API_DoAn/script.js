document.addEventListener('DOMContentLoaded', () => {
  const products = JSON.parse(localStorage.getItem('products')) || [
      { name: 'Sample Product 1', price: '10' },
      { name: 'Sample Product 2', price: '20' }
  ];

  const renderProducts = () => {
      const productList = document.getElementById('product-list');
      if (productList) {
          productList.innerHTML = '';
          products.forEach((product, index) => {
              const productCard = document.createElement('div');
              productCard.className = 'col-md-4 product-card';
              productCard.innerHTML = `
                  <div class="card">
                      <div class="card-body">
                          <h5 class="card-title">${product.name}</h5>
                          <p class="card-text">$${product.price}</p>
                          <a href="edit_product.html?id=${index}" class="btn btn-primary">Edit</a>
                          <button onclick="deleteProduct(${index})" class="btn btn-danger">Delete</button>
                      </div>
                  </div>
              `;
              productList.appendChild(productCard);
          });
      }
  };

  const deleteProduct = (index) => {
      products.splice(index, 1);
      localStorage.setItem('products', JSON.stringify(products));
      renderProducts();
  };

  if (document.getElementById('add-product-form')) {
      document.getElementById('add-product-form').addEventListener('submit', (e) => {
          e.preventDefault();
          const name = document.getElementById('name').value;
          const price = document.getElementById('price').value;
          products.push({ name, price });
          localStorage.setItem('products', JSON.stringify(products));
          window.location.href = 'index.html';
      });
  }

  if (document.getElementById('edit-product-form')) {
      const urlParams = new URLSearchParams(window.location.search);
      const productId = urlParams.get('id');
      const product = products[productId];

      document.getElementById('product-id').value = productId;
      document.getElementById('name').value = product.name;
      document.getElementById('price').value = product.price;

      document.getElementById('edit-product-form').addEventListener('submit', (e) => {
          e.preventDefault();
          const id = document.getElementById('product-id').value;
          products[id].name = document.getElementById('name').value;
          products[id].price = document.getElementById('price').value;
          localStorage.setItem('products', JSON.stringify(products));
          window.location.href = 'index.html';
      });
  }

  renderProducts();
;
})

