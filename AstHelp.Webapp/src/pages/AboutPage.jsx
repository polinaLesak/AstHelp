import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchUser } from '../entities/user/api/userApi';

const AboutPage = () => {
  const dispatch = useDispatch();
  const user = useSelector((state) => state.user.data);

  useEffect(() => {
    dispatch(fetchUser(1)); // Fetch user with ID 1
  }, [dispatch]);

  if (!user) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h1>About Page</h1>
      <h1>Welcome, {user.name}</h1>
    </div>
  );
};

export default AboutPage;
