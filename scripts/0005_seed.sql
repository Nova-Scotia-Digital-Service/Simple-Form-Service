--"a7b65d0f-5b87-4050-a5ef-ef79ef0ec753" -- special patient program
--"170e324d-7aa9-4faf-8b42-5a7f6363fda5" -- early childhood development service
--"17fcd5c7-84cf-4cdc-bb2f-5649396ae8c6" -- drug information system
--"0d9fe6dc-a167-4fe1-9de4-dac996a44ac8" -- child care subsidy review
--"a84c16c0-5ea6-4cc0-87ae-2fdd575715e0" -- child care subsidy

DO $$
/*
DECLARE
	specialPatientProgramTemplateId UUID = gen_random_uuid();
	earlyChildhoodDevelopmentServiceTemplateId UUID = gen_random_uuid();
	drugInformationSystemTemplateId UUID = gen_random_uuid();
	childCareSubsidyReviewTemplateId UUID = gen_random_uuid();
	childCareSubsidyTemplateId UUID = gen_random_uuid();
*/
BEGIN
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES (/*specialPatientProgramTemplateId*/'a7b65d0f-5b87-4050-a5ef-ef79ef0ec753', '{ "Identifier": { "GUID": "a7b65d0f-5b87-4050-a5ef-ef79ef0ec753", "FriendlyName": "Special Patient Program" }, "AuthorizedUsers": [ { "User": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "User": "Inica.Yang@devtestns.onmicrosoft.com" }, { "User": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ], "AdminNotifyEmailAddresses": [ { "EmailAddress": "Craig.Robinson@novascotia.ca" }, { "EmailAddress": "Inica.Yang@novascotia.ca" }, { "EmailAddress": "Kevin.Armstrong@novascotia.ca" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), 'a7b65d0f-5b87-4050-a5ef-ef79ef0ec753', '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "ConfirmationEmailAddresses": [ { "EmailAddress": "Craig.Robinson@novascotia.ca" }, { "EmailAddress": "Inica.Yang@novascotia.ca" }, { "EmailAddress": "Kevin.Armstrong@novascotia.ca" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES (/*earlyChildhoodDevelopmentServiceTemplateId*/'170e324d-7aa9-4faf-8b42-5a7f6363fda5', '{ "Identifier": { "GUID": "170e324d-7aa9-4faf-8b42-5a7f6363fda5", "FriendlyName": "Early Childhood Development Service" }, "AuthorizedUsers": [ { "User": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "User": "Inica.Yang@devtestns.onmicrosoft.com" }, { "User": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ], "AdminNotifyEmailAddresses": [ { "EmailAddress": "Craig.Robinson@novascotia.ca" }, { "EmailAddress": "Inica.Yang@novascotia.ca" }, { "EmailAddress": "Kevin.Armstrong@novascotia.ca" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), '170e324d-7aa9-4faf-8b42-5a7f6363fda5', '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "ConfirmationEmailAddresses": [ { "EmailAddress": "Craig.Robinson@novascotia.ca" }, { "EmailAddress": "Inica.Yang@novascotia.ca" }, { "EmailAddress": "Kevin.Armstrong@novascotia.ca" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES (/*drugInformationSystemTemplateId*/'17fcd5c7-84cf-4cdc-bb2f-5649396ae8c6', '{ "Identifier": { "GUID": "17fcd5c7-84cf-4cdc-bb2f-5649396ae8c6", "FriendlyName": "Drug Information System" }, "AuthorizedUsers": [ { "User": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "User": "Inica.Yang@devtestns.onmicrosoft.com" }, { "User": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ], "AdminNotifyEmailAddresses": [ { "EmailAddress": "Craig.Robinson@novascotia.ca" }, { "EmailAddress": "Inica.Yang@novascotia.ca" }, { "EmailAddress": "Kevin.Armstrong@novascotia.ca" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), '17fcd5c7-84cf-4cdc-bb2f-5649396ae8c6', '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "ConfirmationEmailAddresses": [ { "EmailAddress": "Craig.Robinson@novascotia.ca" }, { "EmailAddress": "Inica.Yang@novascotia.ca" }, { "EmailAddress": "Kevin.Armstrong@novascotia.ca" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES (/*childCareSubsidyReviewTemplateId*/'0d9fe6dc-a167-4fe1-9de4-dac996a44ac8', '{ "Identifier": { "GUID": "0d9fe6dc-a167-4fe1-9de4-dac996a44ac8", "FriendlyName": "Child Care Subsidy Review"  }, "AuthorizedUsers": [ { "User": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "User": "Inica.Yang@devtestns.onmicrosoft.com" }, { "User": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ], "AdminNotifyEmailAddresses": [ { "EmailAddress": "Craig.Robinson@novascotia.ca" }, { "EmailAddress": "Inica.Yang@novascotia.ca" }, { "EmailAddress": "Kevin.Armstrong@novascotia.ca" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), '0d9fe6dc-a167-4fe1-9de4-dac996a44ac8', '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "ConfirmationEmailAddresses": [ { "EmailAddress": "Craig.Robinson@novascotia.ca" }, { "EmailAddress": "Inica.Yang@novascotia.ca" }, { "EmailAddress": "Kevin.Armstrong@novascotia.ca" } ] }');
	INSERT INTO public."Form_Template"(	"Id", "Data")
	VALUES (/*childCareSubsidyTemplateId*/'a84c16c0-5ea6-4cc0-87ae-2fdd575715e0', '{ "Identifier": { "GUID": "a84c16c0-5ea6-4cc0-87ae-2fdd575715e0", "FriendlyName": "Child Care Subsidy"  }, "AuthorizedUsers": [ { "User": "Craig.Robinson@devtestns.onmicrosoft.com" }, { "User": "Inica.Yang@devtestns.onmicrosoft.com" }, { "User": "Kevin.Armstrong@devtestns.onmicrosoft.com" } ], "AdminNotifyEmailAddresses": [ { "EmailAddress": "Craig.Robinson@novascotia.ca" }, { "EmailAddress": "Inica.Yang@novascotia.ca" }, { "EmailAddress": "Kevin.Armstrong@novascotia.ca" } ] }');
	INSERT INTO public."Form_Submission"("Id", "Template_Id", "Data")
	VALUES (gen_random_uuid(), 'a84c16c0-5ea6-4cc0-87ae-2fdd575715e0', '{ "FormItems": [ { "Name": "Email", "Value": "johndoe@gmail.com" }, { "Name": "Name", "Value": "John Doe" }, { "Name": "Phone", "Value": "902-000-000" }, { "Name": "Submission Type", "Value": "New Application" } ], "CreateDate": "2022-01-05 4:39:08 PM", "CreateUser": "MockHttpContextUser", "TemplateId": "a88da4e2-c1b0-4994-ba23-3c5c63a43b48", "UpdateDate": "2022-01-05 4:39:08 PM", "UpdateUser": "MockHttpContextUser", "SubmissionId": " ca68881f-d0d9-4048-9672-9856a5143537", "DateSubmitted": "2022-01-05 4:39:08 PM", "SubmissionStatus": "INITIALIZED", "DocumentReferences": [ { "DocumentId": "random-guid", "TemplateId": "template-id-guid" }, { "DocumentId": "some-other-random-guid", "TemplateId": "some-other-template-id-guid"	} ], "ConfirmationEmailAddresses": [ { "EmailAddress": "Craig.Robinson@novascotia.ca" }, { "EmailAddress": "Inica.Yang@novascotia.ca" }, { "EmailAddress": "Kevin.Armstrong@novascotia.ca" } ] }');
END $$

/*
SELECT "Id", "Data"	FROM public."Form_Template";	
SELECT "Id", "Template_Id", "Data" FROM public."Form_Submission";
*/	

/*
DELETE FROM public."Form_Submission";
DELETE FROM public."Form_Template";
*/
	


	