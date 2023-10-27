import { useAppDispatch, useAppSelector } from "@/hooks";
import { translate } from "@/lib/translate";
import { onCreatePackageVersion } from "@/store/package-store";
import { setTitle } from "@/store/shared-store";
import { Box, Button, FormControl, FormHelperText, FormLabel, Grid, MenuItem, Paper, Select, TextField } from "@mui/material";
import { useFormik } from "formik";
import { useEffect } from "react";
import * as yup from 'yup';

const validationSchema = yup.object({
  packageId: yup
    .number()
    .required('YOU NEED TO SELECT A PACKAGE'),
  versionNumber: yup
    .string()
    .matches(/^(\d+\.)?(\d+\.)?(\*|\d+)$/, 'VERSION NUMBER IS NOT VALID')
    .required('YOU NEED TO ENTER A VERSION NUMBER')
});

const PackageVersionView = () => {
  const dispatch = useAppDispatch();
  const packages = useAppSelector(state => state.packageList.packages);

  const formik = useFormik({
    initialValues: {
      packageId: undefined,
      versionNumber: ''
    },
    validationSchema,
    onSubmit: (values) => {
      dispatch(onCreatePackageVersion(values.packageId!, values.versionNumber));
    }
  })

  useEffect(() => {
    dispatch(setTitle(translate('Page_Title_Versions', 'Versions')));
  });

  return (
    <Box mt={2}>
      <Grid container spacing={3}>
        <Grid item md={5}>
          <Paper style={{ padding: '30px' }}>
            <Grid item md={6}>
              <form onSubmit={formik.handleSubmit}>
                <FormControl error={formik.touched.packageId && !!formik.errors.packageId}>
                  <FormLabel>Package</FormLabel>
                  <Select
                    value={formik.values.packageId}
                    onChange={(e) => formik.setFieldValue('packageId', e.target.value)}
                    style={{ height: 40, width: 250 }}
                  >
                    {packages.map(x => {
                      return <MenuItem key={x.id} value={x.id}>{x.name}</MenuItem>;
                    })}
                  </Select>
                  {formik.touched.packageId && !!formik.errors.packageId && <FormHelperText>{formik.errors.packageId}</FormHelperText>}
                </FormControl>
                <FormControl error={formik.touched.versionNumber && !!formik.errors.versionNumber} style={{ marginTop: 30 }}>
                  <FormLabel>Version</FormLabel>
                  <TextField
                    value={formik.values.versionNumber}
                    onChange={(e) => formik.setFieldValue('versionNumber', e.target.value)}
                    error={formik.touched.versionNumber && !!formik.errors.versionNumber}
                    style={{ height: 40, width: 250 }}
                    inputProps={{ style: { padding: 10 } }}
                  />
                  {formik.touched.versionNumber && !!formik.errors.versionNumber && <FormHelperText>{formik.errors.versionNumber}</FormHelperText>}
                </FormControl>
                <Box mt={3}>
                  <Button variant="contained" color="primary" type="submit">
                    Save
                  </Button>
                </Box>
              </form>
            </Grid>
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
};

export default PackageVersionView;
